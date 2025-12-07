using UnityEngine;

public class BossScript : MonoBehaviour
{
    public static int vida = 30;

    void Update()
    {
       if(vida <= 0)
       {
           Destroy(gameObject);
        }
    }
}
