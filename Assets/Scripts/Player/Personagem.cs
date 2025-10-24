using UnityEngine;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{
	Rigidbody2D _rb;
	float Speed = 250;
	Vector2 Dir;

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
	    _rb.linearVelocity = Dir * Speed * Time.deltaTime;
	}
}
