using UnityEngine;
using UnityEngine.InputSystem;

public class AlavancaScript : MonoBehaviour
{
    bool playerPodeInteragir = false;

    void Update()
    {
        if (playerPodeInteragir && !PlataformaScript.isAlavancaAtivada && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AlavancaAtiva();
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

    void AlavancaAtiva()
    {
        PlataformaScript.isAlavancaAtivada = true;
        print("alavanca");
    }

    public void SetActivatedFromRestore()
    {
        PlataformaScript.isAlavancaAtivada = true;
    }
}

