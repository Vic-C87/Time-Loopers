using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject[] myEnemyPrefabs;

    int mySpawnIndex = 0;

    [SerializeField] Transform myEnemyTarget;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnEnemy(int anIndex = -1)
    {
        if (mySpawnIndex < myEnemyPrefabs.Length && anIndex == -1) 
        {
            GameObject enemy = Instantiate(myEnemyPrefabs[mySpawnIndex], transform.position, Quaternion.identity);
            enemy.GetComponent<Seeker>().Target = myEnemyTarget;
            mySpawnIndex++;
        }
        else if (anIndex != -1)
        {
            GameObject enemy = Instantiate(myEnemyPrefabs[anIndex], transform.position, Quaternion.identity);
            enemy.GetComponent<Seeker>().Target = myEnemyTarget;

        }
    }
}
