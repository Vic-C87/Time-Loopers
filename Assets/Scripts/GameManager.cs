using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager sInstance;

    [field: SerializeField] public int currentBiomass { get; set; }
    [SerializeField] private TextMeshProUGUI myBiomassText;
    [SerializeField] private TextMeshProUGUI myNextRound;
    [SerializeField] private TextMeshProUGUI myWeakness;
    [SerializeField] private TextMeshProUGUI myRoundDuration;
    [SerializeField] private bool myDoublePistolManUnlocked = false;
    [SerializeField] private bool mySniperManUnlocked = false;
    [SerializeField] private bool myFlamethrowerManUnlocked = false;
    [SerializeField] private ETowerType myTowerType;
    [SerializeField] List<ETowerType> myBoughtTowers = new List<ETowerType>();

    private void Awake()
    {
        currentBiomass = 1000;

        if (sInstance != null && sInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myBiomassText.text = $"Biomass: {currentBiomass}";
    }

    public void AddTower(int value)
    {
        switch (value)
        {
            case (int)ETowerType.PistolMan:
                if (currentBiomass >= 10)
                {
                    myBoughtTowers.Add(ETowerType.PistolMan);
                    currentBiomass -= 10;
                }
                else
                {
                    Debug.Log("Not enough biomass");
                }
                break;

            case (int)ETowerType.DoublePistolMan:
                if (currentBiomass >= 50 && myDoublePistolManUnlocked == true)
                {
                    myBoughtTowers.Add(ETowerType.DoublePistolMan);
                    currentBiomass -= 50;
                }
                else
                {
                    Debug.Log("Not enough biomass");
                }
                break;

            case (int)ETowerType.SniperMan:
                if (currentBiomass >= 70 && mySniperManUnlocked == true)
                {
                    myBoughtTowers.Add(ETowerType.SniperMan);
                    currentBiomass -= 70;
                }
                else
                {
                    Debug.Log("Not enough biomass");
                }
                break;

            case (int)ETowerType.FlamethrowerMan:
                if (currentBiomass >= 100 && myFlamethrowerManUnlocked == true)
                {
                    myBoughtTowers.Add(ETowerType.FlamethrowerMan);
                    currentBiomass -= 100;
                }
                else
                {
                    Debug.Log("Not enough biomass");
                }
                break;

            default:
                myBoughtTowers.Add(ETowerType.Null);
                break;
        }
    }

    public void RemoveTower(int value)
    {
        switch (value)
        {
            case (int)ETowerType.PistolMan:
                if (myBoughtTowers.Contains(ETowerType.PistolMan))
                {
                    myBoughtTowers.Remove(ETowerType.PistolMan);
                    currentBiomass += 5;
                }
                else
                {
                    Debug.Log("Can't sell what you don't have");
                }
                break;

            case (int)ETowerType.DoublePistolMan:
                if (myBoughtTowers.Contains(ETowerType.DoublePistolMan))
                {
                    myBoughtTowers.Remove(ETowerType.DoublePistolMan);
                    currentBiomass += 25;
                }
                else
                {
                    Debug.Log("Can't sell what you don't have");
                }
                break;

            case (int)ETowerType.SniperMan:
                if (myBoughtTowers.Contains(ETowerType.SniperMan))
                {
                    myBoughtTowers.Remove(ETowerType.SniperMan);
                    currentBiomass += 35;
                }
                else
                {
                    Debug.Log("Can't sell what you don't have");
                }
                break;

            case (int)ETowerType.FlamethrowerMan:
                if (myBoughtTowers.Contains(ETowerType.FlamethrowerMan))
                {
                    myBoughtTowers.Remove(ETowerType.FlamethrowerMan);
                    currentBiomass += 50;
                }
                else
                {
                    Debug.Log("Can't sell what you don't have");
                }
                break;

            default:
                Debug.Log("Can't sell what you don't have");
                break;
        }
    }


    public void UnlockTurret(int value)
    {
        switch (value) 
        { 
            case(int)EUnlockTurret.DoublePistolManUnlock:
                if (currentBiomass >= 100 && myDoublePistolManUnlocked != true)
                {
                    currentBiomass -= 100;
                    myDoublePistolManUnlocked = true;
                }
                else
                {
                    Debug.Log("Not enough biomass");
                }
                break;

            case (int)EUnlockTurret.SniperManUnlock:
                if (currentBiomass >= 140 && mySniperManUnlocked != true)
                {
                    currentBiomass -= 140;
                    mySniperManUnlocked = true;
                }
                else
                {
                    Debug.Log("Not enough biomass");
                }
                break;

            case (int)EUnlockTurret.FlamethrowerManUnlock:
                if (currentBiomass >= 200 && myFlamethrowerManUnlocked != true)
                {
                    currentBiomass -= 200;
                    myFlamethrowerManUnlocked = true;
                }
                else
                {
                    Debug.Log("Not enough biomass");
                }
                break;

            default:
                Debug.Log("Nope");
                break;
        }
    }

    public enum EUnlockTurret
    {
        DoublePistolManUnlock,
        SniperManUnlock,
        FlamethrowerManUnlock
    }
}
/*
float myTier1UpgradeAttackSpeed = -0.1f;
float myTier2UpgradeAttackSpeed = -0.15f;
int myTier1UpgradeRange = 1;
int myTier2UpgradeRange = 2;
int myTier1UpgradeDamage = 1;
int myTier2UpgradePower = 2;
*/
