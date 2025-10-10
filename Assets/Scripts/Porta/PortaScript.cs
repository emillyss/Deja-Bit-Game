using UnityEngine;

public class PortaScript : MonoBehaviour
{
    [SerializeField] int quantidadeInimigos = 1;
    BoxCollider2D porta;
    void Start()
    {
        porta = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(quantidadeInimigos == 0)
        {
            //aplicar animação de porta abrindo
            porta.enabled = false;
        }
    }
}
