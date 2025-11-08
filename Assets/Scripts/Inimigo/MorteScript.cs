using UnityEngine;

public class MorteScript : MonoBehaviour
{
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bau"))
        {
            Transform pai = collision.transform.parent;
            Rigidbody2D rb = pai.GetComponent<Rigidbody2D>();
            if(rb.linearVelocity != Vector2.zero)
            {
                MensagensScript.isMorto = true;
                AudioManager.instance.PlayInimigoMorte();
                PortaScript.quantidadeInimigosVivos -= 1;
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
