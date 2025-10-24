using UnityEngine;

public class CaixaScript : MonoBehaviour
{
    PolygonCollider2D collider2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider2D = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Personagem.isVulneravel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Personagem.isVulneravel = false;
        }
    }
}
