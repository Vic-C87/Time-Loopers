using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject myPistolManPrefab;
    [SerializeField] GameObject myDoublePistolManPrefab;

    [SerializeField] GameObject myCurrentPickedTower;

    [SerializeField] LayerMask myLayerMask;
    [SerializeField] LayerMask myUnwalkableMask;


    [SerializeField] float myTowerProximityRadius;
    [SerializeField] float myTowerDropHeight;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && myCurrentPickedTower != null) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, myLayerMask))
            {   
                if (!Physics.CheckSphere(hit.point, myTowerProximityRadius, myUnwalkableMask))
                    PlaceTower(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            myCurrentPickedTower = null;
        }

    }

    public void PickPistolMan()
    {
        myCurrentPickedTower = myPistolManPrefab;
    }

    public void PickDoublePistolMan()
    {
        myCurrentPickedTower = myDoublePistolManPrefab;
    }

    public void PlaceTower(Vector3 aPosition)
    {
        Debug.Log("Placing tower at: " + aPosition);
        aPosition.y += myTowerDropHeight;
        Instantiate<GameObject>(myCurrentPickedTower, aPosition, Quaternion.identity);
    }
}

