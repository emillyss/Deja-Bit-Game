using UnityEngine;
using UnityEngine.SceneManagement;

public class RochaScript : MonoBehaviour
{
    [SerializeField] int dano = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
            BossScript.vida -= dano;
        }

    }
}
