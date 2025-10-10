using UnityEngine;
using UnityEngine.SceneManagement;

public class InimigoScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float waitTime = 2f;
    [SerializeField] float moveRadius = 10f;
    [SerializeField] BoxCollider2D areaCollider;

    Rigidbody2D rb;
    Vector3 target;
    bool isMovimentando = false;
    bool isPerigo = false;
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

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        target = new Vector3(randomX, randomY, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player"))
        //{
        //    SceneManager.LoadScene(0);
        //}
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject.layer == LayerMask.NameToLayer("Matavel"))
        {
            isPerigo = true;
            collision.transform.parent.position += Vector3.down * speed * Time.deltaTime;
            print("Achou");
        }
    }
}
