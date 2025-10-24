using UnityEngine;
using UnityEngine.InputSystem;

public class BauScript : MonoBehaviour
{
    [SerializeField] int quantidadeDeEspacamento = 5;

    bool playerPodeInteragir = false;
    bool isAberto = false;
    public BoxCollider2D colliderTrigger;
    public BoxCollider2D colliderEmpurra;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderTrigger.enabled = true;
        colliderEmpurra.enabled = false;
    }

    void Update()
    {
        if (playerPodeInteragir && !isAberto && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AbrirBau();
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
        isAberto = true;
        Personagem.isChave = true;
        SelectionManager.diskCapacity += quantidadeDeEspacamento;
        colliderTrigger.enabled = false;
        colliderEmpurra.enabled = true;
        rb.gravityScale = 1;
        print("bau");
        MensagensScript.isLetal = true;
    }
}
