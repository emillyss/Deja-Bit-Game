using UnityEngine;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{
    Animator _playerSpriteAnimator;
    SpriteRenderer _spriteRenderer;
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
        _playerSpriteAnimator = GetComponentInChildren<Animator>();

        // Tenta pegar o SpriteRenderer para virar o personagem
        if (_playerSpriteAnimator != null)
        {
            _spriteRenderer = _playerSpriteAnimator.GetComponent<SpriteRenderer>();
        }
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    void FixedUpdate()
    {
        Movimentar();
        AtualizarAnimacao(); // Chama a função de animação a cada frame de física
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
        // Movimento na grade
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

    // --- FUNÇÃO ATUALIZADA COM LÓGICA DE SUBIDA ---
    void AtualizarAnimacao()
    {
        if (_playerSpriteAnimator == null || _spriteRenderer == null) return;

        // Verifica se há input de movimento
        bool temMovimentoHorizontal = Mathf.Abs(Dir.x) > 0;
        bool temMovimentoVertical = Mathf.Abs(Dir.y) > 0;

        // 1. Lógica de Subir (Escada)
        // Ativa se: Pode subir (está na escada) E tem movimento vertical (cima ou baixo)
        bool estaSubindo = canMoveUp && temMovimentoVertical;
        
        _playerSpriteAnimator.SetBool("Subindo", estaSubindo);

        // 2. Lógica de Correr (Chão)
        // Ativa se: Tem movimento horizontal E NÃO está subindo a escada
        // (Isso impede que ele "corra" enquanto sobe a escada)
        bool estaCorrendo = temMovimentoHorizontal && !estaSubindo;
        
        _playerSpriteAnimator.SetBool("Correndo", estaCorrendo);

        // 3. Lógica de Virar (Flip)
        if (Dir.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (Dir.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}