using UnityEngine;

public class SavableObject : MonoBehaviour
{
    public int memoryWeight = 10;

    SpriteRenderer sr;
    Color originalColor;
    MaterialPropertyBlock mpb;
    int colorPropID;

    public GameObject outlineObject;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        originalColor = sr.color;
        mpb = new MaterialPropertyBlock();
        colorPropID = Shader.PropertyToID("_Color");
        sr.GetPropertyBlock(mpb);

        if (outlineObject != null) outlineObject.SetActive(false);
    }

    // captura o estado atual (pos, rot, vel)
    public SavedState CaptureState()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 vel = Vector3.zero;
        if (rb != null) vel = rb.velocity;
        return new SavedState(transform.position, transform.rotation, vel, memoryWeight, gameObject.name);
    }

    // destaque visual simples (altera cor) 
    public void Highlight(bool on)
    {    
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
            rb.velocity = s.velocity;
            rb.simulated = true;
        }
    }
}

