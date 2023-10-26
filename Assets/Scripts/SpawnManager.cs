using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject mySpawns;
    SpawnPoint[] mySpawnPoints;

    void Awake()
    {
        mySpawnPoints = mySpawns.GetComponentsInChildren<SpawnPoint>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            mySpawnPoints[0].SpawnEnemy(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mySpawnPoints[1].SpawnEnemy(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mySpawnPoints[2].SpawnEnemy(0);
        }
    }
}
