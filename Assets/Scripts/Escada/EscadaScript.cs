using UnityEngine;

public class EscadaScript : MonoBehaviour
{
    [SerializeField] float forcaGravidade = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Personagem player = collision.gameObject.GetComponentInParent<Personagem>();
            Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            player.canMoveUp = true;
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Personagem player = collision.gameObject.GetComponentInParent<Personagem>();
            player.canMoveUp = false;
            collision.gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = forcaGravidade;
        }
    }

}
