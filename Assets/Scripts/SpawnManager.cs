using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject mySpawns;
    SpawnPoint[] mySpawnPoints;

    float mySpawnDelay = 3f;
    float myTimeSinceSpawn;

    int mySpawnCounter = 0;

    bool myStartSpawn = false;

    int myMaxSpawns;

    void Awake()
    {
        mySpawnPoints = mySpawns.GetComponentsInChildren<SpawnPoint>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.realtimeSinceStartup - myTimeSinceSpawn >= mySpawnDelay && myStartSpawn) 
        {
            Spawn();
        }
    }

    public void StartBattle(int aMaxSpawns)
    {
        myStartSpawn = true;
        myMaxSpawns = aMaxSpawns;
    }

    void Spawn()
    {
        mySpawnPoints[0].SpawnEnemy(GameManager.sInstance.GetGameLevel());
        mySpawnPoints[1].SpawnEnemy(GameManager.sInstance.GetGameLevel());
        mySpawnPoints[2].SpawnEnemy(GameManager.sInstance.GetGameLevel());
        myTimeSinceSpawn = Time.realtimeSinceStartup;
        mySpawnCounter++;
        if (mySpawnCounter >= myMaxSpawns)
        {
            myStartSpawn = false;
        }
    }
}
