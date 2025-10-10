using UnityEngine;

public class BloquearBauScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bau"))
        {
            Rigidbody2D bauRigidbody = collision.GetComponent<Rigidbody2D>();
            bauRigidbody.gravityScale = 0;
            bauRigidbody.linearVelocity = Vector2.zero;
        }
    }
}
