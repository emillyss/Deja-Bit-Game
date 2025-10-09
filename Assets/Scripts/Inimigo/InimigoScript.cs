using UnityEngine;

public class InimigoScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float waitTime = 2f;
    [SerializeField] float moveRadius = 10f;
    [SerializeField] BoxCollider2D areaCollider;

    Rigidbody2D rb;
    Vector3 startPosition;
    Vector3 target;
    bool isMovimentando = false;
    float waitTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        RandomChoice();
    }

    void Update()
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

    void RandomChoice()
    {
        Bounds bounds = areaCollider.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        target = new Vector3(randomX, randomY, 0f);
    }
}
