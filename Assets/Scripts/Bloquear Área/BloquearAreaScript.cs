using UnityEngine;

public class BloquearAreaScript : MonoBehaviour
{
    [SerializeField] GameObject paredeInvisivel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            paredeInvisivel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            paredeInvisivel.SetActive(false);
        }
    }
}
