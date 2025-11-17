using UnityEngine;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{
    Rigidbody2D _rb;
    float Speed = 250;
    Vector2 Dir;
    public bool canMoveUp = false;

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
        
        if (canMoveUp)
        {
            velocity = Dir * Speed * Time.deltaTime;
        }
        else
        {
            velocity.x = Dir.x * Speed * Time.deltaTime;
            velocity.y = _rb.linearVelocity.y; 
        }
        
        _rb.linearVelocity = velocity;
    }
}