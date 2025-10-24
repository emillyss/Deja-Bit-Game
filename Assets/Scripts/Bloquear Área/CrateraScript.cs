using UnityEngine;
using UnityEngine.SceneManagement;

public class CrateraScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !Personagem.isVulneravel)
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
