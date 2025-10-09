using UnityEngine;

public class SavableObject : MonoBehaviour
{
    public int memoryWeight = 10;

    SpriteRenderer sr;
    Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) originalColor = sr.color;
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
        if (sr == null) return;
        sr.color = on ? Color.cyan : originalColor;
    }
}

