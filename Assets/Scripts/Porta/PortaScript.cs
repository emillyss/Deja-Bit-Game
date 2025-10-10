using UnityEngine;

public class PortaScript : MonoBehaviour
{
    static public int quantidadeInimigosVivos = 1;
    BoxCollider2D porta;
    void Start()
    {
        porta = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(quantidadeInimigosVivos == 0)
        {
            //aplicar animação de porta abrindo
            porta.enabled = false;
        }
    }
}
