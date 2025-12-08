using UnityEngine;
using UnityEngine.InputSystem;

public class BotãoSpawnCaixaScript1 : MonoBehaviour
{
    bool playerPodeInteragir = false;
    public static bool isPressionado = false;
    [SerializeField] GameObject caixa;
    [SerializeField] Transform posicaoSpawn;

    private void Start()
    {
        isPressionado = false;
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
            print("botão");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPodeInteragir = false;
            print("saiu");
        }
    }

    void PressionarBotao()
    {
        if (!isPressionado)
        {
            isPressionado = true;

            AudioManager.instance.PlayBotaoNormal();
            print("botão");
            Instantiate(caixa, posicaoSpawn.position, posicaoSpawn.rotation);
        }
    }
}
