using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab, healthGlobePrefab, staminaGlobePrefab;

    public void DropItems()
    {
        int randmonNum = Random.Range(1, 5);

        if (randmonNum == 1)
        {
            Instantiate(staminaGlobePrefab, transform.position, Quaternion.identity);
        }

        if (randmonNum == 2)
        {
            Instantiate(healthGlobePrefab, transform.position, Quaternion.identity);
        }

        if (randmonNum == 3)
        {
            int randmonGoldCoinNum = Random.Range(1, 4);

            for (int i = 0; i <= randmonGoldCoinNum; i++)
            {
                Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
