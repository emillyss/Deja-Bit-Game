using UnityEngine;

public class LiberarBoolRebobinarScript : MonoBehaviour
{
    bool isEnviado = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isEnviado)
        {
            MensagensScript.isRebobinar = true;
            isEnviado = true;
        }
    }
}
