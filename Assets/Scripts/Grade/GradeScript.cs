using UnityEngine;

public class GradeScript : MonoBehaviour
{
    [SerializeField] float forcaGravidade = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Personagem player = collision.gameObject.GetComponentInParent<Personagem>();
            Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();

            player.gradeCount++;

            if (player.gradeCount == 1)
            {
                player.canMoveOnGrade = true;
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Personagem player = collision.gameObject.GetComponentInParent<Personagem>();

            player.gradeCount--;

            if (player.gradeCount <= 0)
            {
                player.gradeCount = 0;
                player.canMoveOnGrade = false;
                collision.gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = forcaGravidade;
            }
        }
    }
}
