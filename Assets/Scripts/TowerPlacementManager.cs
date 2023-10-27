using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] float myPlacementDelay;
    float myLastPlacementTime;
    [SerializeField] GameObject myPistolManPrefab;
    [SerializeField] GameObject myDoublePistolManPrefab;
    [SerializeField] GameObject mySniperManPrefab;
    [SerializeField] GameObject myFlamethrowerManPrefab;

    [SerializeField] GameObject myCurrentPickedTower;

    [SerializeField] LayerMask myLayerMask;
    [SerializeField] LayerMask myUnwalkableMask;


    [SerializeField] float myTowerProximityRadius;
    [SerializeField] float myTowerDropHeight;

    [SerializeField] Vector2 GridWorldSize;
    [SerializeField] float myNodeRadius;
    float myNodeDiameter;
    int myGridSizeX, myGridSizeY;
    Vector3[,] myGrid;
    bool[,] myOccupiedGrid;

    AudioSource myAudioSource;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        myLastPlacementTime = Time.realtimeSinceStartup;
        myNodeDiameter = myNodeRadius * 2;
        myGridSizeX = Mathf.RoundToInt(GridWorldSize.x / myNodeDiameter);
        myGridSizeY = Mathf.RoundToInt(GridWorldSize.y / myNodeDiameter);
        CreatePlacementGrid();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && myCurrentPickedTower != null) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {   
                PlaceTower(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            myCurrentPickedTower = null;
        }
    }

    void CreatePlacementGrid()
    {
        myGrid = new Vector3[myGridSizeX, myGridSizeY];
        myOccupiedGrid = new bool[myGridSizeX, myGridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;
        for (int x = 0; x < myGridSizeX; x++)
        {
            for (int y = 0; y < myGridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + (Vector3.right * (x * myNodeDiameter + myNodeRadius) + Vector3.forward * (y * myNodeDiameter + myNodeRadius));
                myGrid[x, y] = worldPoint;
                myOccupiedGrid[x, y] = false;
            }
        }
    }

    public bool NodeFromWorldPoint(Vector3 aWorldPosition, out Vector3 aPlacementPosition)
    {
        float percentX = (aWorldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (aWorldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((myGridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((myGridSizeY - 1) * percentY);
        aPlacementPosition = myGrid[x, y];
        bool isOccupied = myOccupiedGrid[x, y];
        if (!isOccupied)
        {
            myOccupiedGrid[x, y] = true;
        }
        return isOccupied;
    }

    public void PickPistolMan()
    {
        myCurrentPickedTower = myPistolManPrefab;
    }

    public void PickDoublePistolMan()
    {
        myCurrentPickedTower = myDoublePistolManPrefab;
    }

    public void PickSniperMan()
    {
        myCurrentPickedTower = mySniperManPrefab;
    }

    public void PickFlamethrowerMan()
    {
        myCurrentPickedTower = myFlamethrowerManPrefab;
    }

    public void PlaceTower(Vector3 aPosition)
    {
        if (Time.realtimeSinceStartup - myLastPlacementTime >= myPlacementDelay)
        {
            if (!NodeFromWorldPoint(aPosition, out Vector3 aPlacementPosition)) 
            { 
                myLastPlacementTime = Time.realtimeSinceStartup;
                Debug.Log("Placing tower at: " + aPlacementPosition);
                aPlacementPosition.y += myTowerDropHeight;
                Instantiate<GameObject>(myCurrentPickedTower, aPlacementPosition, Quaternion.identity);
                if (SoundManager.sInstance.GetAudioClip(EClipName.SpaceManDrop, out AudioClip aClip))
                {
                    myAudioSource.clip = aClip;
                    myAudioSource.Play();
                }
            }
        }
    }
}

