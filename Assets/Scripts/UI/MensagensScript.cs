using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MensagensScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bemVindo;
    [SerializeField] TextMeshProUGUI bemVindo1;
    [SerializeField] TextMeshProUGUI bemVindo2;
    [SerializeField] TextMeshProUGUI mensagemIntrodutoria;
    [SerializeField] TextMeshProUGUI mensagemRebobinar;
    [SerializeField] TextMeshProUGUI mensagemEspacamento;
    [SerializeField] TextMeshProUGUI mensagemObjetosLetais;
    [SerializeField] TextMeshProUGUI mensagemDetectarPerigo;
    [SerializeField] TextMeshProUGUI mensagemItensRecebidos;
    [SerializeField] TextMeshProUGUI mensagemMorteInimigo;
    [SerializeField] TextMeshProUGUI mensagemNovoEstado;
    [SerializeField] float tempoDeEspera = 5f;

    bool isInicio = false;
    public static bool isRebobinar = false;
    public static bool isEspacamento = false;
    public static bool isLetal = false;
    public static bool isPerigo = false;
    public static bool isItens = false;
    public static bool isMorto = false;
    public static bool isNovoEstado = false;
    float tempoDeTela = 0f;
    float tempoDeEsperaInicial = 0f;
    float waitTimer = 0f;
    bool isExibindo = false;
    List<TextMeshProUGUI> listaDeExibicao = new List<TextMeshProUGUI>();

    private void Start()
    {
        listaDeExibicao.Add(bemVindo);
        listaDeExibicao.Add(bemVindo1);
        listaDeExibicao.Add(bemVindo2);
        tempoDeEsperaInicial = Time.time;
    }

    void Update()
    {
        if (Time.time > tempoDeEsperaInicial + 10f && !isInicio)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            isInicio = true;
            listaDeExibicao.Add(mensagemIntrodutoria);
        }

        if (isRebobinar)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            listaDeExibicao.Add(mensagemRebobinar);
            isRebobinar = false;
        }

        if (isEspacamento)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            listaDeExibicao.Add(mensagemEspacamento);
            isEspacamento = false;
        }

        if (isLetal)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            listaDeExibicao.Add(mensagemObjetosLetais);
            isLetal = false;
        }

        if (isPerigo)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            listaDeExibicao.Add(mensagemDetectarPerigo);
            isPerigo = false;
        }

        if (isNovoEstado)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            listaDeExibicao.Add(mensagemNovoEstado);
            isNovoEstado = false;
        }

        if (isMorto)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            listaDeExibicao.Add(mensagemMorteInimigo);
            isMorto = false;
        }

        if (isItens)
        {
            tempoDeTela = Time.time + tempoDeEspera;
            listaDeExibicao.Add(mensagemItensRecebidos);
            isItens = false;
        }

        if (!isExibindo)
        {
            if (listaDeExibicao.Count > 0)
            {
                tempoDeTela = Time.time + tempoDeEspera;
                isExibindo = true;
                waitTimer = tempoDeEspera;
                AudioManager.instance.PlayCaixaDeTexto();
                listaDeExibicao[0].gameObject.SetActive(true);
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                listaDeExibicao[0].gameObject.SetActive(false);
                listaDeExibicao.RemoveAt(0);
                isExibindo = false;
            }
        }
    }
}
