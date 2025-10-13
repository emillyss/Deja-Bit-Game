using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SelectionManager : MonoBehaviour
{
    public Key toggleKey = Key.R;
    public LayerMask savableLayer;
    public int diskCapacity = 20;
    public List<SavedState> savedStates = new List<SavedState>();
    public Image viewportBackground;

    bool selectionMode = false;
    SavableObject lastHighlighted = null;

    // novo: mapa runtime de instanceId -> SavableObject
    Dictionary<int, SavableObject> instanceMap = new Dictionary<int, SavableObject>();
    
    public Transform savedContent;       
    public GameObject savedSlotPrefab;  

    // runtime
    List<GameObject> currentSlots = new List<GameObject>();


    void Awake()
    {
        // preenche o mapa com os SavableObjects atuais na cena
        RefreshInstanceMap();
        
        // garantir estado inicial: fundo desligado
        if (viewportBackground != null) viewportBackground.gameObject.SetActive(false);
    }

    // público útil caso queira atualizar (ex.: instância dinâmica)
    public void RefreshInstanceMap()
    {
        instanceMap.Clear();
        var all = FindObjectsOfType<SavableObject>();
        foreach (var s in all)
        {
            int id = s.gameObject.GetInstanceID();
            if (!instanceMap.ContainsKey(id)) instanceMap.Add(id, s);
            else instanceMap[id] = s;
        }
    }

    void Update()
    {
        // Toggle selection mode com tecla R (teclado)
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            ToggleSelectionMode();
        }

        // Right click: ativa ou desativa o modo seleção (ignore se clicou na UI)
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (!EventSystem.current || !EventSystem.current.IsPointerOverGameObject())
            {
                if (selectionMode) CancelSelectionMode();
                else ToggleSelectionMode();
            }
        }

        if (!selectionMode) return;

        // highlight object under cursor
        HandleHighlightUnderCursor();

        // left click selects (ignora clicks sobre UI)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!EventSystem.current || !EventSystem.current.IsPointerOverGameObject())
            {
                TrySelectUnderCursor();
            }
        }
    }

    void SetViewportBackground(bool on)
    {
        if (viewportBackground != null)
        {
            // ativa/desativa o GameObject da Image para evitar bloquear eventos quando off
            viewportBackground.gameObject.SetActive(on);
        }
    }


    void ToggleSelectionMode()
    {
        selectionMode = !selectionMode;
        if (!selectionMode) ClearHighlight();
        Debug.Log("Selection mode: " + (selectionMode ? "ON" : "OFF"));

        // Atualiza visibilidade/itens do painel quando muda o modo seleção
        if (selectionMode)
        {
            UpdateSavedPanel();
        }
        else
        {
            ClearSavedPanel();
        }
        
        SetViewportBackground(selectionMode);
    }

    void CancelSelectionMode()
    {
        selectionMode = false;
        ClearHighlight();
        ClearSavedPanel();

        // garantir que o background do viewport seja desligado quando cancelar
        SetViewportBackground(false);

        Debug.Log("Selection cancelled.");
    }

    // detecta qual objeto o mouse está apontando e manda ele brilhar
    void HandleHighlightUnderCursor()
    {
        if (Camera.main == null || Mouse.current == null) return;

        Vector2 mp = Mouse.current.position.ReadValue();
        Vector3 screenPoint = new Vector3(mp.x, mp.y, Mathf.Abs(Camera.main.transform.position.z));
        Vector3 wp = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 p2 = new Vector2(wp.x, wp.y);

        Collider2D col = Physics2D.OverlapPoint(p2, savableLayer);
        if (col != null)
        {
            // tenta pegar no mesmo GameObject do collider
            SavableObject so = col.GetComponent<SavableObject>();
            // fallback: procura no parent (caso o collider esteja em um filho)
            if (so == null) so = col.GetComponentInParent<SavableObject>();

            if (so != null)
            {
                if (lastHighlighted != null && lastHighlighted != so) lastHighlighted.Highlight(false);
                lastHighlighted = so;
                lastHighlighted.Highlight(true);
                return;
            }
        }

        // if nothing hit, clear highlight
        if (lastHighlighted != null)
        {
            lastHighlighted.Highlight(false);
            lastHighlighted = null;
        }
    }

    void TrySelectUnderCursor()
    {
        if (Camera.main == null || Mouse.current == null) return;

        Vector2 mp = Mouse.current.position.ReadValue();
        Vector3 screenPoint = new Vector3(mp.x, mp.y, Mathf.Abs(Camera.main.transform.position.z));
        Vector3 wp = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 p2 = new Vector2(wp.x, wp.y);

        Collider2D col = Physics2D.OverlapPoint(p2, savableLayer);
        if (col == null)
        {
            Debug.Log("No savable object here.");
            return;
        }

        // tenta obter o componente no mesmo objeto do collider
        SavableObject sav = col.GetComponent<SavableObject>();
        // fallback: se o collider está em um filho, procura no pai
        if (sav == null)
        {
            sav = col.GetComponentInParent<SavableObject>();
            if (sav != null) Debug.Log("Found SavableObject in parent: " + sav.gameObject.name);
        }

        if (sav == null)
        {
            Debug.Log("Object hit is not savable.");
            return;
        }

        // capture state (agora inclui instanceId vindo do SavableObject)
        SavedState state = sav.CaptureState();

        // registre/atualize o mapa para garantir lookup futuro
        int id = state.instanceId;
        if (!instanceMap.ContainsKey(id)) instanceMap.Add(id, sav);
        else instanceMap[id] = sav;

        // check capacity (simple sum)
        int used = 0;
        foreach (var s in savedStates) used += s.memoryWeight;

        if (used + state.memoryWeight > diskCapacity)
        {
            Debug.Log("Not enough disk capacity to save this object.");
            return;
        }

        savedStates.Add(state);
        Debug.Log($"Saved '{state.objName}' id={state.instanceId} at {state.position}. Used: {used + state.memoryWeight}/{diskCapacity} MB");
        Debug.Log($"Saved. total savedStates = {savedStates.Count}");

        // atualiza painel (se estiver aberto)
        if (selectionMode) UpdateSavedPanel();

        ClearHighlight();
    }


    void ClearSavedPanel()
    {
        for (int i = 0; i < currentSlots.Count; i++)
        {
            if (currentSlots[i] != null) Destroy(currentSlots[i]);
        }
        currentSlots.Clear();
    }

    void UpdateSavedPanel()
    {
    	Debug.Log($"UpdateSavedPanel: savedStates.Count = {savedStates.Count}");
        if (savedContent == null || savedSlotPrefab == null) return;

        ClearSavedPanel();

        for (int i = 0; i < savedStates.Count; i++)
        {
            var state = savedStates[i];
            GameObject go = Instantiate(savedSlotPrefab, savedContent, false);
            currentSlots.Add(go);

            var slot = go.GetComponent<SavedSlot>();
            if (slot != null)
            {
                string label = $"{state.objName} - {state.memoryWeight} MB";
                int idx = i; // capture o índice local para o callback

                // tenta obter ícone via instanceMap (se existir), senão passa null
                Sprite icon = null;
                SavableObject target = null;
                if (state.instanceId != 0 && instanceMap.TryGetValue(state.instanceId, out target))
                {
                    icon = target != null ? target.GetIcon() : null;
                }

                slot.Setup(label, () => { RestoreSavedState(idx); }, icon, () => { DeleteSavedState(idx); });

            }
            else
            {
                // fallback simples: se prefab não tiver SavedSlot, tenta setar Text/Button
                var txt = go.GetComponentInChildren<UnityEngine.UI.Text>();
                if (txt != null) txt.text = $"{state.objName} - {state.memoryWeight} MB";
                var btn = go.GetComponentInChildren<UnityEngine.UI.Button>();
                if (btn != null)
                {
                    int idx = i;
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(() => RestoreSavedState(idx));
                }
            }
        }
    }

	// ---------- Deleta um savedState por índice e atualiza o painel
	public void DeleteSavedState(int index)
	{
	    if (index < 0 || index >= savedStates.Count)
	    {
		Debug.LogWarning("DeleteSavedState: invalid index.");
		return;
	    }

	    var removed = savedStates[index];
	    savedStates.RemoveAt(index);
	    Debug.Log($"Deleted saved state '{removed.objName}' (instanceId {removed.instanceId}).");

	    // atualiza o painel (recria os slots e ajusta indices)
	    UpdateSavedPanel();
	}

    void ClearHighlight()
    {
        if (lastHighlighted != null)
        {
            lastHighlighted.Highlight(false);
            lastHighlighted = null;
        }
    }

    // ---------- NOVO: restaurar um savedState por índice (usa instanceId)
    public bool RestoreSavedState(int index)
    {
        if (index < 0 || index >= savedStates.Count)
        {
            Debug.LogWarning("RestoreSavedState: invalid index.");
            return false;
        }

        SavedState s = savedStates[index];

        SavableObject target = null;
        if (s.instanceId != 0 && instanceMap.TryGetValue(s.instanceId, out target))
        {
            target.RestoreState(s);
            Debug.Log($"Restored '{s.objName}' by instanceId {s.instanceId}.");
            return true;
        }

        // fallback: procurar por nome (se mapa não tem ou objeto foi destruído)
        var all = FindObjectsOfType<SavableObject>();
        foreach (var so in all)
        {
            if (so.gameObject.name == s.objName)
            {
                target = so;
                break;
            }
        }

        if (target != null)
        {
            // atualizar mapa para usos futuros
            instanceMap[s.instanceId] = target;
            target.RestoreState(s);
            Debug.Log($"Restored '{s.objName}' by name fallback.");
            return true;
        }

        Debug.LogWarning($"RestoreSavedState: target for '{s.objName}' not found (instanceId {s.instanceId}).");
        return false;
    }
}

