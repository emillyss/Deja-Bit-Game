using UnityEngine;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{
    Rigidbody2D _rb;
    float Speed = 250;
    Vector2 Dir;
    public bool canMoveUp = false;
    public bool canMoveOnGrade = false;
    public int gradeCount = 0;

    public static bool isVulneravel = false;
    static public bool isChave = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movimentar();
    }

    void OnMove(InputValue inputValue)
    {
        Dir = inputValue.Get<Vector2>();
    }

    void Movimentar()
    {
        Vector2 velocity = Vector2.zero;

        // Movimento na escada (vertical livre)
        if (canMoveUp)
        {
            velocity = Dir * Speed * Time.deltaTime;
        }
        // Movimento na grade (todas as direções)
        else if (canMoveOnGrade)
        {
            velocity = Dir * Speed * Time.deltaTime;
        }
        // Movimento normal (só horizontal)
        else
        {
            velocity.x = Dir.x * Speed * Time.deltaTime;
            velocity.y = _rb.linearVelocity.y;
        }

        _rb.linearVelocity = velocity;
    }
}
