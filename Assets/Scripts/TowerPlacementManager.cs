using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    int myAvailablePistolMan = 0;
    int myAvailableDoublePistolMan = 0;
    int myAvailableSniperMan = 0;
    int myAvailableFlamethrowerMan = 0;

    float myAttackSpeedBonus;
    float myRangeBonus;
    float myDamageBonus;

    [SerializeField] TextMeshProUGUI myBuyPistolMan;
    [SerializeField] TextMeshProUGUI myBuyDoublePistolMan;
    [SerializeField] TextMeshProUGUI myBuySniperMan;
    [SerializeField] TextMeshProUGUI myBuyFlamethrowerMan;

    [SerializeField] Button[] myButtonsToHide;

    [SerializeField] float myLevelTime;
    float myLevelStartTime;

    bool myTimeIsUp = false;

    bool myGameStarted = false;

    bool myIsPlacing = true;

    [SerializeField] TextMeshProUGUI myTimerText;

    ETowerType myCurrentPickedType;

    [SerializeField] TextMeshProUGUI myWinLevelText;
    [SerializeField] GameObject myWinLevel;
    [SerializeField] string myWinText;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        myLastPlacementTime = Time.realtimeSinceStartup;
        myNodeDiameter = myNodeRadius * 2;
        myGridSizeX = Mathf.RoundToInt(GridWorldSize.x / myNodeDiameter);
        myGridSizeY = Mathf.RoundToInt(GridWorldSize.y / myNodeDiameter);
        CreatePlacementGrid();
    }

    private void Start()
    {
        SetAvailabelUnits();
        myWinLevel.SetActive(false);
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
            myCurrentPickedType = ETowerType.Null;
        }

        if (Time.realtimeSinceStartup - myLevelStartTime >= myLevelTime && !myIsPlacing) 
        {
            Debug.Log("Time is UP!");
            myTimeIsUp = true;
            myGameStarted = false;
            myTimerText.text = "Time Left: " + 0;
            Time.timeScale = 0f;
            myWinLevel.SetActive(true);
            myWinLevelText.text = myWinText + " " + GameManager.sInstance.LevelEarnings + " Biomass";
        }
        if (myGameStarted)
            myTimerText.text = "Time Left: " + (myLevelTime - (int)(Time.realtimeSinceStartup - myLevelStartTime)).ToString();
    }

    public void HideBuyCanvas()
    {
        foreach (Button button in myButtonsToHide) 
        {
            button.gameObject.SetActive(false);
        }
        myLevelStartTime = Time.realtimeSinceStartup;
        myGameStarted = true;
        myIsPlacing = false;
        if (SoundManager.sInstance.GetAudioClip(EClipName.BattleMusic, out AudioClip anAudioClip)) 
        {
            GameManager.sInstance.myAudioSource.clip = anAudioClip;
            GameManager.sInstance.myAudioSource.Play();
        }
    }

    void SetAvailabelUnits()
    {
        myAttackSpeedBonus = GameManager.sInstance.GetAttackSpeedBonus();
        myRangeBonus = GameManager.sInstance.GetRangeBonus();
        myDamageBonus = GameManager.sInstance.GetDamageBonus();

        ETowerType[] towers = GameManager.sInstance.GetOwnedTowers().ToArray();

        foreach(ETowerType tower in towers) 
        {
            switch(tower) 
            {
                case ETowerType.PistolMan:
                    myAvailablePistolMan++;
                    break;
                case ETowerType.DoublePistolMan:
                    myAvailableDoublePistolMan++; 
                    break;
                case ETowerType.SniperMan: 
                    myAvailableSniperMan++; 
                    break;
                case ETowerType.FlamethrowerMan:
                    myAvailableFlamethrowerMan++;
                    break;
                default:
                    break;
            }
        }

        myBuyPistolMan.text = "Pistol Man x" + myAvailablePistolMan;
        myBuyDoublePistolMan.text = "Double Pistol Man x" + myAvailableDoublePistolMan;
        myBuySniperMan.text = "Sniper Man x" + myAvailableSniperMan;
        myBuyFlamethrowerMan.text = "Flamethrower Man x" + myAvailableFlamethrowerMan;
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
        if (myAvailablePistolMan > 0)
        {
            myCurrentPickedTower = myPistolManPrefab;
            myCurrentPickedType = ETowerType.PistolMan;
        }
    }

    public void PickDoublePistolMan()
    {
        if (myAvailableDoublePistolMan > 0)
        {
            myCurrentPickedTower = myDoublePistolManPrefab;
            myCurrentPickedType = ETowerType.DoublePistolMan;
        }
    }

    public void PickSniperMan()
    {
        if (myAvailableSniperMan > 0)
        {
            myCurrentPickedTower = mySniperManPrefab;
            myCurrentPickedType = ETowerType.SniperMan;
        }
    }

    public void PickFlamethrowerMan()
    {
        if (myAvailableFlamethrowerMan > 0)
        {
            myCurrentPickedTower = myFlamethrowerManPrefab;
            myCurrentPickedType = ETowerType.FlamethrowerMan;
        }
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

                switch (myCurrentPickedType) 
                {
                    case ETowerType.PistolMan:
                        myAvailablePistolMan--;
                        myBuyPistolMan.text = "Pistol Man x" + myAvailablePistolMan;
                        if (myAvailablePistolMan < 1)
                        {
                            myCurrentPickedTower = null;
                            myCurrentPickedType = ETowerType.Null;
                        }
                            
                        break;
                    case ETowerType.DoublePistolMan:
                        myAvailableDoublePistolMan--;
                        myBuyDoublePistolMan.text = "Double Pistol Man x" + myAvailableDoublePistolMan;
                        if (myAvailableDoublePistolMan < 1)
                        {
                            myCurrentPickedTower = null;
                            myCurrentPickedType = ETowerType.Null;
                        }
                        break;
                    case ETowerType.SniperMan:
                        myAvailableSniperMan--;
                        myBuySniperMan.text = "Sniper Man x" + myAvailableSniperMan;
                        if (myAvailableSniperMan < 1)
                        {
                            myCurrentPickedTower = null;
                            myCurrentPickedType = ETowerType.Null;
                        }
                        break;
                    case ETowerType.FlamethrowerMan:
                        myAvailableFlamethrowerMan--;
                        myBuyFlamethrowerMan.text = "Flamethrower Man x" + myAvailableFlamethrowerMan;
                        if (myAvailableFlamethrowerMan < 1)
                        {
                            myCurrentPickedTower = null;
                            myCurrentPickedType = ETowerType.Null;
                        }
                        break;
                    default:
                        myCurrentPickedTower = null;
                        myCurrentPickedType = ETowerType.Null;
                        break;
                }

            }
        }
    }
}

