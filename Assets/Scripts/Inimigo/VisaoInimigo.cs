using UnityEngine;
using UnityEngine.SceneManagement;

public class VisaoInimigo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se o player estive no campo de visao
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
