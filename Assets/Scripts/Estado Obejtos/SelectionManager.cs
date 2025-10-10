using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; 

public class SelectionManager : MonoBehaviour
{
    public Key toggleKey = Key.R; 
    public LayerMask savableLayer;
    public int diskCapacity = 20;
    public List<SavedState> savedStates = new List<SavedState>();

    bool selectionMode = false;
    SavableObject lastHighlighted = null;

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

    void ToggleSelectionMode()
    {
        selectionMode = !selectionMode;
        if (!selectionMode) ClearHighlight();
        Debug.Log("Selection mode: " + (selectionMode ? "ON" : "OFF"));
    }

    void CancelSelectionMode()
    {
        selectionMode = false;
        ClearHighlight();
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

        // capture state
        SavedState state = sav.CaptureState();

        // check capacity (simple sum)
        int used = 0;
        foreach (var s in savedStates) used += s.memoryWeight;

        if (used + state.memoryWeight > diskCapacity)
        {
            Debug.Log("Not enough disk capacity to save this object.");
            return;
        }

        savedStates.Add(state);
        Debug.Log($"Saved '{state.objName}' at {state.position}. Used: {used + state.memoryWeight}/{diskCapacity} MB");

        // optionally exit selection mode after a save:
        selectionMode = false;
        ClearHighlight();
    }

    void ClearHighlight()
    {
        if (lastHighlighted != null)
        {
            lastHighlighted.Highlight(false);
            lastHighlighted = null;
        }
    }
}

