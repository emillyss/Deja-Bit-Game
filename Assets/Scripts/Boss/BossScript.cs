using UnityEngine;

public class BossScript : MonoBehaviour
{
    public static int vida = 30;
    [SerializeField] GameObject portal;

    void Update()
    {
       if(vida <= 0)
        {
            portal.SetActive(true);
            Destroy(gameObject);
        }
    }
}
