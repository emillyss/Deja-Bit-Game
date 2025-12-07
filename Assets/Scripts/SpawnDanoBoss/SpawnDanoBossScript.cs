using System.Collections.Generic;
using UnityEngine;

public class SpawnDanoBossScript : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPosition;
    [SerializeField] GameObject rochaPrefab;
    public static bool cairRocha = false;

    void Update()
    {
        if (cairRocha)
        {
            SpawnPedra();
        }

    }

    void SpawnPedra()
    {
        int randomPosition = Random.Range(0, spawnPosition.Count);

        Instantiate(rochaPrefab, spawnPosition[randomPosition].transform.position, Quaternion.identity);
        cairRocha = false;
    }
}
