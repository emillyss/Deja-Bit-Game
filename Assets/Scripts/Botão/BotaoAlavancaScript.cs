using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class BotaoAlavancaScript : MonoBehaviour
{
    [SerializeField] GameObject parede;
    [SerializeField] GameObject escada;
    [SerializeField] TextMeshProUGUI portaAberta;
    [SerializeField] TextMeshProUGUI portaFechada;
    [SerializeField] float waitTime = 5f;

    public static bool isPressionado = false;
    Animator animator;
    float waitTimer = 0f;

    void Start()
    {
        escada.SetActive(false);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0f)
        {
            portaAberta.gameObject.SetActive(false);
            portaFechada.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Caixa"))
        {
            isPressionado = true;
            PlataformaScript.isAlavancaAtivada = true;
            animator.SetBool("isApertado", true);
            parede.SetActive(false);
            escada.SetActive(true);
            AudioManager.instance.PlayCaixaDeTexto();
            portaAberta.gameObject.SetActive(true);
            portaFechada.gameObject.SetActive(false);
            waitTimer = waitTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Caixa"))
        {
            isPressionado = false;
            PlataformaScript.isAlavancaAtivada = false;
            animator.SetBool("isApertado", false);
            parede.SetActive(true);
            escada.SetActive(false);
            AudioManager.instance.PlayCaixaDeTexto();
            portaAberta.gameObject.SetActive(false);
            portaFechada.gameObject.SetActive(true);
            waitTimer = waitTime;
        }
    }

    void PressionarBotao()
    {
        if (!isPressionado)
        {
            AudioManager.instance.PlayBotaoPressao();
            isPressionado = true;
            print("botão");
            // aplicações ações do botão
        }
    }

}
