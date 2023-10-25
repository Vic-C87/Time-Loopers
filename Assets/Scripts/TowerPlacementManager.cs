using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject myPistolManPrefab;

    [SerializeField] GameObject myCurrentPickedTower;

    [SerializeField] LayerMask myLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && myCurrentPickedTower != null) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, myLayerMask))
            {                
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

    public void PlaceTower(Vector3 aPosition)
    {
        Debug.Log("Placing tower at: " + aPosition);
        aPosition.y += 3;
        Instantiate<GameObject>(myCurrentPickedTower, aPosition, Quaternion.identity);
        //myCurrentPickedTower = null;
    }
}

