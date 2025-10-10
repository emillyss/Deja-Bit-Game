using UnityEngine;

public class PlataformaDescerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    static public bool isDescer = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDescer)
        {
            collision.transform.parent.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
