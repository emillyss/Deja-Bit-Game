using UnityEngine;
using UnityEngine.InputSystem;

public class BotaoClicavelScript : MonoBehaviour
{
    bool playerPodeInteragir = false;
    public static bool isPressionado = false;
    [SerializeField] GameObject escada;
    [SerializeField] GameObject chao;
    [SerializeField] GameObject plataforma;
    [SerializeField] GameObject alavanca;
    [SerializeField] Transform posicaoSpawn;

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
            Destroy(plataforma);
            Destroy(alavanca);
            Instantiate(escada, posicaoSpawn.position, posicaoSpawn.rotation);
            chao.SetActive(true);
        }
    }
}
