using Unity.Cinemachine;
using UnityEngine;

public class PortaEntreMiniFaseScript : MonoBehaviour
{
    [SerializeField] CinemachineCamera cameraAtual;
    [SerializeField] CinemachineCamera proximaCamera;
    [SerializeField] GameObject trocaCamera;
    [SerializeField] GameObject parede;
    bool isSom;

    void Update()
    {
        if (BotaoScript.isPressionado)
        {
            if (!isSom)
            {
                AudioManager.instance.PlayPorta();
                isSom = true;
            }
        }
        else
        {
            isSom = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraAtual.gameObject.SetActive(false);

            proximaCamera.gameObject.SetActive(true);

            MensagensScript.isEspacamento = true;
            parede.SetActive(true);
            trocaCamera.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
