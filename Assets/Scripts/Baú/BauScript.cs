using UnityEngine;
using UnityEngine.InputSystem;

public class BauScript : MonoBehaviour
{
    PlayerInput inputInteragir;
    bool playerPodeInteragir = false;
    bool isAberto = false;

    void Start()
    {
        inputInteragir = GetComponent<PlayerInput>();
        inputInteragir.enabled = false;
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

    void AbrirBau()
    {
        isAberto = true;
        inputInteragir.enabled = false; 
        print("bau");
    }
}
