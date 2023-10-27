using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject[] myEnemyPrefabs;

    [SerializeField] Transform myEnemyTarget;

    public void SpawnEnemy(int aLevel)
    {
        if (aLevel == 1) 
        {
            GameObject enemy = Instantiate(myEnemyPrefabs[0], transform.position, Quaternion.identity);
            enemy.GetComponent<Seeker>().Target = myEnemyTarget;
        }
        else 
        {
            int spawnIndex = Random.Range(0, myEnemyPrefabs.Length);
            GameObject enemy = Instantiate(myEnemyPrefabs[spawnIndex], transform.position, Quaternion.identity);
            enemy.GetComponent<Seeker>().Target = myEnemyTarget;
        }
    }
}
