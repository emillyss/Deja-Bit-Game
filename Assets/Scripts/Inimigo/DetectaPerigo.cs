using UnityEngine;
using UnityEngine.Rendering;

public class DetectaPerigo : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float circleRadius = 3f;
    [SerializeField] LayerMask detectableLayers = -1;
    [SerializeField] float velocidadeFuga = 8f;
    [SerializeField] BoxCollider2D areaCollider;

    Vector3 target;

    void Update()
    {
        DetectaCollidersEmVolta();
    }

    void DetectaCollidersEmVolta()
    {
        Collider2D[] objetosDetectados = Physics2D.OverlapCircleAll(transform.position, circleRadius, detectableLayers);

        bool playerDetected = false;
        bool matavelDetected = false;
        GameObject playerObject = null;

        foreach (Collider2D obj in objetosDetectados)
        {
            if (obj.gameObject == this.gameObject) continue;

            if (obj.CompareTag("Player"))
            {
                playerDetected = true;
                playerObject = obj.gameObject;
            }

            if (obj.gameObject.layer == LayerMask.NameToLayer("Matavel"))
            {
                matavelDetected = true;
            }
        }

        if (playerDetected && matavelDetected)
        {
            if (!InimigoScript.isPerigo) 
            {
                InimigoScript.isPerigo = true;
                print("Player e objeto matável detectados juntos no círculo!");

                Vector3 direcaoFuga = (transform.parent.position - playerObject.transform.position).normalized;
                Vector3 posicaoFugaIdeal = transform.parent.position + direcaoFuga * velocidadeFuga;

                target = LimitarPosicaoNaArea(posicaoFugaIdeal);

                float step = speed * Time.deltaTime;
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, target, step);
                print(target);
                if (Vector3.Distance(transform.parent.position, target) < 0.1f)
                {
                    InimigoScript.isPerigo = false;
                }
            }
        }
        else
        {
            InimigoScript.isPerigo = false;
        }
    }

    Vector3 LimitarPosicaoNaArea(Vector3 posicao)
    {
        Bounds bounds = areaCollider.bounds;

        float x = Mathf.Clamp(posicao.x, bounds.min.x, bounds.max.x);
        float y = Mathf.Clamp(posicao.y, bounds.min.y, bounds.max.y);

        return new Vector3(x, y, 0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, circleRadius);
    }
}
