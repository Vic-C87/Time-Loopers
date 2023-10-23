using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject mySpawns;
    [SerializeField] SpawnPoint[] mySpawnPoints;

    void Awake()
    {
        mySpawnPoints = mySpawns.GetComponentsInChildren<SpawnPoint>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            mySpawnPoints[0].SpawnEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mySpawnPoints[1].SpawnEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mySpawnPoints[2].SpawnEnemy();
        }
    }
}
