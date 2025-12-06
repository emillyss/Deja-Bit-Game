using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DIalogoTelaHistoriaScript : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> listaDeExibicao;
    [SerializeField] GameObject portal;
    float waitTimer;
    float tempoDeEspera = 20f;

    private void Start()
    {
        portal.SetActive(false);
        if (listaDeExibicao.Count > 0)
        {
            listaDeExibicao[0].gameObject.SetActive(true);
            waitTimer = tempoDeEspera;
        }
    }

    void Update()
    {
        for(int i = 0; i< listaDeExibicao.Count; i++)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0 && listaDeExibicao[i].gameObject.activeSelf)
            {
                listaDeExibicao[i].gameObject.SetActive(false);
                if (i + 1 < listaDeExibicao.Count)
                {
                    waitTimer = tempoDeEspera;
                    listaDeExibicao[i + 1].gameObject.SetActive(true);
                }
                else
                {
                    portal.SetActive(true);
                }
            }
        }
        if (listaDeExibicao[listaDeExibicao.Count - 1].gameObject.activeSelf)
        {
        }
    }
}
