using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MensagensScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mensagemIntrodutoria;
    [SerializeField] TextMeshProUGUI mensagemRebobinar;
    [SerializeField] TextMeshProUGUI mensagemEspacamento;
    [SerializeField] TextMeshProUGUI mensagemObjetosLetais;
    [SerializeField] TextMeshProUGUI mensagemDetectarPerigo;
    [SerializeField] float tempoDeEspera = 5f;

    bool isInicio = false;
    public static bool isRebobinar = false;
    public static bool isEspacamento = false;
    public static bool isLetal = false;
    public static bool isPerigo = false;
    float tempoDeTela = 0f;
    float tempoDeEsperaInicial = 0f;

    private void Start()
    {
        tempoDeEsperaInicial = Time.time;
    }

    void Update()
    {
        if (Time.time > tempoDeEsperaInicial * 2f && !isInicio)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            isInicio = true;
            mensagemIntrodutoria.gameObject.SetActive(true);
        }
        else
        {
            if (Time.time >= tempoDeTela)
            {
                mensagemIntrodutoria.gameObject.SetActive(false);
            }
        }

        if (isRebobinar)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            mensagemRebobinar.gameObject.SetActive(true);
            isRebobinar = false;
        }
        else
        {
            if (Time.time >= tempoDeTela)
            {
                mensagemRebobinar.gameObject.SetActive(false);
            }
        }

        if (isEspacamento)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            mensagemEspacamento.gameObject.SetActive(true);
            isEspacamento = false;
        }
        else
        {
            if (Time.time >= tempoDeTela)
            {
                mensagemEspacamento.gameObject.SetActive(false);
            }
        }

        if (isLetal)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            mensagemObjetosLetais.gameObject.SetActive(true);
            isLetal = false;
        }
        else
        {
            if (Time.time >= tempoDeTela)
            {
                mensagemObjetosLetais.gameObject.SetActive(false);
            }
        }

        if (isPerigo)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            mensagemDetectarPerigo.gameObject.SetActive(true);
            isPerigo = false;
        }
        else
        {
            if (Time.time >= tempoDeTela)
            {
                mensagemDetectarPerigo.gameObject.SetActive(false);
            }
        }
    }
}
