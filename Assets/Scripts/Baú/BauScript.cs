using UnityEngine;
using UnityEngine.InputSystem;

public class BauScript : MonoBehaviour
{
    [SerializeField] int quantidadeDeEspacamento = 20;

    bool playerPodeInteragir = false;
    bool isAberto = false;
    public BoxCollider2D colliderTrigger;
    public BoxCollider2D colliderEmpurra;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderTrigger.enabled = true;
        colliderEmpurra.enabled = true;
    }

    void Update()
    {
        if (playerPodeInteragir && !isAberto && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AbrirBau();
        }
        if(colliderTrigger.enabled == true)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPodeInteragir = true;
            print("eeee");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPodeInteragir = false;
        }
    }

    void AbrirBau()
    {
        AudioManager.instance.PlayBau();
        isAberto = true;
        Personagem.isChave = true;
        SelectionManager.diskCapacity += quantidadeDeEspacamento;
        colliderTrigger.enabled = false;
        colliderEmpurra.enabled = true;
        rb.gravityScale = 1;
        rb.mass = 20;
        print("bau");
        MensagensScript.isLetal = true;
        MensagensScript.isItens = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo") && rb.linearVelocity != Vector2.zero)
        {
            MensagensScript.isMorto = true;
            AudioManager.instance.PlayInimigoMorte();
            PortaScript.quantidadeInimigosVivos -= 1;
            Destroy(collision.gameObject);
        }
    }
}
