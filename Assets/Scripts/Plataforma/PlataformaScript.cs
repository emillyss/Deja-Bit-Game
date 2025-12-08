using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class PlataformaScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float moveRadius = 10f;
    [SerializeField] float deslocamento;

    static public bool isAlavancaAtivada = false;
    Vector3 positionAtual;
    Vector3 target;
    bool isMovimentando = false;
    float waitTimer = 0f;

    void Start()
    {
        positionAtual = transform.position;
        target = new Vector3(transform.position.x, deslocamento, transform.position.z);
    }

    void Update()
    {
        if (isAlavancaAtivada)
        {
            Subir();
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                Descer();
            }
        }

    }

    void Subir()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            waitTimer = waitTime;
        }
    }

    void Descer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, positionAtual, step);
        PlataformaDescerScript.isDescer = true;

        if (Vector3.Distance(transform.position, positionAtual) < 0.1f)
        {
            isAlavancaAtivada = false;
            PlataformaDescerScript.isDescer = false;
        }
    }

}
