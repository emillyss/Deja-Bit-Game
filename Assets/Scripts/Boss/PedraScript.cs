using UnityEngine;
using UnityEngine.SceneManagement;

public class PedraScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("BossFight");
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            SpawnDanoBossScript.cairRocha = true;
            Destroy(gameObject);
        }
    }
}
