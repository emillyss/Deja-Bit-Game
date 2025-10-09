using UnityEngine;

public class BauScript : MonoBehaviour
{
    BoxCollider2D areaInteracao;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        areaInteracao = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

        }
    }
}
