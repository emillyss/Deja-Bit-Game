using UnityEngine;
using UnityEngine.InputSystem;

public class AlavancaScript : MonoBehaviour
{
    bool playerPodeInteragir = false;

    void Update()
    {
        if (playerPodeInteragir && !PlataformaScript1.isAlavancaAtivada && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AlavancaAtiva();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayAlavanca();
            playerPodeInteragir = true;
            print("eeee");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayAlavanca();
            playerPodeInteragir = false;
        }
    }

    void AlavancaAtiva()
    {
        PlataformaScript1.isAlavancaAtivada = true;
        print("alavanca");
    }

    public void SetActivatedFromRestore()
    {
        PlataformaScript1.isAlavancaAtivada = true;
    }
}

