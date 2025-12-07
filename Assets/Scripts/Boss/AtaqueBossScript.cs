using UnityEngine;

public class AtaqueBossScript : MonoBehaviour
{
    [Header("Configurações do Projétil")]
    [SerializeField] GameObject pedraPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float velocidadePedra = 10f;
    [SerializeField] float angulo = 45f;

    [Header("Configurações de Tempo")]
    [SerializeField] float intervaloAtaque = 3f;
    private float proximoAtaque = 0f;

    [Header("Direção")]
    [SerializeField] bool olhandoDireita = true;

    void Update()
    {
        if (Time.time >= proximoAtaque)
        {
            Atacar();
            proximoAtaque = Time.time + intervaloAtaque;
        }
    }

    void Atacar()
    {
        GameObject pedra = Instantiate(pedraPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = pedra.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float anguloRad = angulo * Mathf.Deg2Rad;
            float velocidadeX = Mathf.Cos(anguloRad) * velocidadePedra;
            float velocidadeY = Mathf.Sin(anguloRad) * velocidadePedra;

            float direcao = transform.localScale.x > 0 ? 1 : -1;
            velocidadeX *= direcao;

            rb.linearVelocity = new Vector2(velocidadeX, velocidadeY);
        }

        Destroy(pedra, 5f);
    }


    public void MudarDirecao(bool novaDirecao)
    {
        olhandoDireita = novaDirecao;
    }
}
