using UnityEngine;
using UnityEngine.InputSystem;

public class BotaoScript : MonoBehaviour
{
    bool playerPodeInteragir = false;
    bool isPressionado = false;

    void Update()
    {
        if (playerPodeInteragir && !isPressionado && Keyboard.current.eKey.wasPressedThisFrame && Personagem.isChave)
        {
            PressionarBotao();
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

    void PressionarBotao()
    {
        isPressionado = true;
        print("botão");
        // aplicações ações do botão
    }
}
