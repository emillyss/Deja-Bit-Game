using UnityEngine;
using UnityEngine.InputSystem;

public class BotaoScript : MonoBehaviour
{
    bool playerPodeInteragir = false;
    public static bool isPressionado = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
            animator.SetBool("isApertado", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Caixa"))
        {
            isPressionado = false;
            print("saiu");
            animator.SetBool("isApertado", false);
        }
    }

    void PressionarBotao()
    {
        if (!isPressionado)
        {
            AudioManager.instance.PlayBotaoPressao();
            isPressionado = true;
            print("botão");
            // aplicações ações do botão
        }
    }
}
