using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaScript : MonoBehaviour
{
    [SerializeField] BoxCollider2D bloqueio;
    [SerializeField] string cenaParaCarregar;
    static public int quantidadeInimigosVivos = 1;

    void Update()
    {
        if(quantidadeInimigosVivos == 0)
        {
            //aplicar animação de porta abrindo
            AudioManager.instance.PlayPorta();
            bloqueio.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(cenaParaCarregar);
        }
    }
}
