using UnityEngine;

public class MorteScript : MonoBehaviour
{
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bau"))
        {
            MensagensScript.isMorto = true;
            AudioManager.instance.PlayInimigoMorte();
            PortaScript.quantidadeInimigosVivos -= 1;
            Destroy(transform.parent.gameObject);
        }
    }
}
