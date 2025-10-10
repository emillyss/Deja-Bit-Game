using UnityEngine;
using UnityEngine.InputSystem;

public class BotaoScript : MonoBehaviour
{
    PlayerInput inputInteragir;
    bool playerPodeInteragir = false;
    bool isPressionado = false;

    void Start()
    {
        inputInteragir = GetComponent<PlayerInput>();
        inputInteragir.enabled = false;
    }
    void Update()
    {
        if (playerPodeInteragir && !isPressionado && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PressionarBotao();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPodeInteragir = true;
            inputInteragir.enabled = true;
            print("eeee");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPodeInteragir = false;
            inputInteragir.enabled = false;
        }
    }

    void PressionarBotao()
    {
        isPressionado = true;
        inputInteragir.enabled = false;
        print("botão");
        // aplicações ações do botão
    }
}
