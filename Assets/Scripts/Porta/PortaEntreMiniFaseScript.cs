using Unity.Cinemachine;
using UnityEngine;

public class PortaEntreMiniFaseScript : MonoBehaviour
{
    [SerializeField] CinemachineCamera cameraAtual;
    [SerializeField] CinemachineCamera proximaCamera;
    [SerializeField] Vector3 novaPosicaoPlayer = Vector3.zero;
    [SerializeField] GameObject bloquearAcesso;
    [SerializeField] BoxCollider2D bloqueada;
    [SerializeField] BoxCollider2D portaAcesso;
    bool isSom;
    void Start()
    {
        bloqueada = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (BotaoScript.isPressionado)
        {
            if (!isSom)
            {
                AudioManager.instance.PlayPorta();
                isSom = true;
            }
            bloqueada.enabled = false;
        }
        else
        {
            bloqueada.enabled = true;
            isSom = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraAtual.gameObject.SetActive(false);

            proximaCamera.gameObject.SetActive(true);

            if (novaPosicaoPlayer != Vector3.zero)
            {
                collision.transform.parent.position = novaPosicaoPlayer;
            }

            bloquearAcesso.SetActive(true);
            portaAcesso.enabled = false;
            MensagensScript.isEspacamento = true;
        }
    }
}
