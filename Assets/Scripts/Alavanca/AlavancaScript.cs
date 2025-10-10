using UnityEngine;
using UnityEngine.InputSystem;

public class AlavancaScript : MonoBehaviour
{
    PlayerInput inputInteragir;
    bool playerPodeInteragir = false;

    void Start()
    {
        inputInteragir = GetComponent<PlayerInput>();
        inputInteragir.enabled = false;
    }
    void Update()
    {
        if (playerPodeInteragir && !PlataformaScript.isAlavancaAtivada && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AlavancaAtiva();
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

    void AlavancaAtiva()
    {
        PlataformaScript.isAlavancaAtivada = true;
        inputInteragir.enabled = false;
        print("alavanca");
    }
}
