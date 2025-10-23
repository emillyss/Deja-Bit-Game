using UnityEngine;
using UnityEngine.SceneManagement;
public class InimigoScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float waitTime = 2f;
    [SerializeField] float moveRadius = 10f;
    [SerializeField] BoxCollider2D areaCollider;
    [SerializeField] BoxCollider2D areaProibida;

    Rigidbody2D rb;
    Vector3 target;
    bool isMovimentando = false;
    static public bool isPerigo = false;
    float waitTimer = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RandomChoice();
    }

    void Update()
    {
        if (!isPerigo)
        {
            if (isMovimentando)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, step);
                if (Vector3.Distance(transform.position, target) < 0.1f)
                {
                    isMovimentando = false;
                    waitTimer = waitTime;
                }
            }
            else
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    RandomChoice();
                    isMovimentando = true;
                }
            }
        }
    }
    void RandomChoice()
    {
        Bounds bounds = areaCollider.bounds;
        Vector3 candidateTarget;

        do
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            candidateTarget = new Vector3(randomX, randomY, 0f);
            if (areaProibida.bounds.Contains(candidateTarget))
            {
                MensagensScript.isPerigo = true;
            }
        }
        while (areaProibida.gameObject.activeInHierarchy && areaProibida.bounds.Contains(candidateTarget));

        target = candidateTarget;
    }


}