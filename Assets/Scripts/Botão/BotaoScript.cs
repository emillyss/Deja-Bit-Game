using UnityEngine;
using UnityEngine.InputSystem;

public class BotaoScript : MonoBehaviour
{
    bool playerPodeInteragir = false;
    public static bool isPressionado = false;

    //void Update()
    //{
    //    if (playerPodeInteragir && !isPressionado && Keyboard.current.eKey.wasPressedThisFrame && Personagem.isChave)
    //    {
    //        PressionarBotao();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Caixa"))
        {
            isPressionado = true;
            print("botão");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Caixa"))
        {
            isPressionado = false;
            print("saiu");
        }
    }

    void PressionarBotao()
    {
        if (!isPressionado)
        {
            isPressionado = true;
            print("botão");
            // aplicações ações do botão
        }
    }
}
