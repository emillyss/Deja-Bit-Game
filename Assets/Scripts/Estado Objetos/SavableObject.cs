using UnityEngine;

public class SavableObject : MonoBehaviour
{
    public int memoryWeight = 10;

    public Sprite icon;

    SpriteRenderer sr;
    Color originalColor;
    MaterialPropertyBlock mpb;
    int colorPropID;

    public GameObject outlineObject;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
        {
            // se icon não foi setado no inspector, pega o sprite do SpriteRenderer
            if (icon == null) icon = sr.sprite;

            originalColor = sr.color;
            mpb = new MaterialPropertyBlock();
            colorPropID = Shader.PropertyToID("_Color");
            sr.GetPropertyBlock(mpb);
        }
        else
        {
            Debug.LogWarning($"SavableObject '{gameObject.name}' não encontrou SpriteRenderer em si ou filhos.");
        }

        if (outlineObject != null) outlineObject.SetActive(false);
    }

    // captura o estado atual (pos, rot, vel) — já esperava ter instanceId no SavedState
    public SavedState CaptureState()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 vel = Vector3.zero;
        if (rb != null) vel = rb.linearVelocity;
        int id = gameObject.GetInstanceID();
        return new SavedState(transform.position, transform.rotation, vel, memoryWeight, gameObject.name, id);
    }

    // função simples para fornecer o ícone ao UI
    public Sprite GetIcon()
    {
        // retorna o icon se definido, senão tenta o sprite do SpriteRenderer, senão null
        if (icon != null) return icon;
        if (sr != null) return sr.sprite;
        return null;
    }

    // destaque visual simples (altera cor) 
    public void Highlight(bool on)
    {
        if (sr == null) return;

        sr.GetPropertyBlock(mpb);
        Color target = on ? Color.cyan : originalColor;

        mpb.SetColor(colorPropID, target);
        sr.SetPropertyBlock(mpb);

        return;
    }

    // Restaura o estado
    public void RestoreState(SavedState s)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        transform.position = s.position;
        transform.rotation = s.rotation;

        if (rb != null)
        {
            rb.linearVelocity = s.velocity;
            rb.simulated = true;
        }
    }
}

