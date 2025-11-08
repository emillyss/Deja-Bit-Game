using UnityEngine;

public class MovimentacaoInimigoScript : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    float enemyDir = 1;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = speed * enemyDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bau") || collision.CompareTag("Limite"))
        {
            FlipSprite();
        }
    }
    void FlipSprite()
    {
        print(transform.parent.localScale);
        transform.parent.localScale = new Vector3(-Mathf.Sign(rb.linearVelocityX), 1, 1);
        print(transform.parent.localScale);
        enemyDir *= -1;
    }
}
