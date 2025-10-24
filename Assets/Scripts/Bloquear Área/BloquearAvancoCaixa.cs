using UnityEngine;

public class BloquearAvancoCaixa : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Caixa"))
        {
            AudioManager.instance.PlayCaixaCaindo();
            Transform pai = collision.transform.parent;

            PolygonCollider2D collider2D = pai.GetComponentInChildren<PolygonCollider2D>();
            Rigidbody2D rb = pai.GetComponent<Rigidbody2D>();

            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            collider2D.isTrigger = true;
        }
    }
}
