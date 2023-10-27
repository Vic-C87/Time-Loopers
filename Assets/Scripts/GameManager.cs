using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager sInstance;

    [field: SerializeField] public int currentBiomass { get; set; }
    public int LevelEarnings { get; set; } = 0;
    [SerializeField] private TextMeshProUGUI myBiomassText;
    [SerializeField] private TextMeshProUGUI myNextRound;
    [SerializeField] private TextMeshProUGUI myWeakness;
    [SerializeField] private TextMeshProUGUI myRoundDuration;
    [SerializeField] private bool myDoublePistolManUnlocked = false;
    [SerializeField] private bool mySniperManUnlocked = false;
    [SerializeField] private bool myFlamethrowerManUnlocked = false;
    [SerializeField] private ETowerType myTowerType;
    [SerializeField] List<ETowerType> myBoughtTowers = new List<ETowerType>();
    [SerializeField] private bool myAllAttackSpeedUpgradesBought = false;
    [SerializeField] private bool myAllRangeUpgradesBought = false;
    [SerializeField] private bool myAllDamageUpgradesBought = false;
    [SerializeField] private bool myFirstAttackSpeedUpgradeBought = false;
    [SerializeField] private bool myFirstRangeUpgradeBought = false;
    [SerializeField] private bool myFirstDamageUpgradeBought = false;
    float myAttackSpeedBonus;
    float myRangeBonus;
    float myDamageBonus;

    GameObject myCanvas;

    private void Awake()
    {
        currentBiomass = 5000;

        if (sInstance != null && sInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        myCanvas = GetComponentInChildren<Canvas>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateBiomassText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public List<ETowerType> GetOwnedTowers()
    {
        return myBoughtTowers;
    }

    public float GetAttackSpeedBonus() { return myAttackSpeedBonus; }
    public float GetRangeBonus() {  return myRangeBonus; }
    public float GetDamageBonus() {  return myDamageBonus; }

    public void LoadShop()
    {
        currentBiomass += LevelEarnings;
        SceneManager.LoadScene(1);
        myCanvas.SetActive(true);
    }

    public void ExitShop()
    {
        myCanvas.SetActive(false);
        LevelEarnings = 0;
        SceneManager.LoadScene(2);
    }

    void UpdateBiomassText()
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
        UpdateBiomassText();
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
        UpdateBiomassText();
    }


    public void UnlockTurret(int value)
    {
        switch (value) 
        { 
            case(int)EUnlockTurret.DoublePistolManUnlock:
                if (currentBiomass >= 100 && myDoublePistolManUnlocked != true && myAllAttackSpeedUpgradesBought)
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
                if (currentBiomass >= 140 && mySniperManUnlocked != true && myAllRangeUpgradesBought)
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
                if (currentBiomass >= 200 && myFlamethrowerManUnlocked != true && myAllDamageUpgradesBought)
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
        UpdateBiomassText();
    }

    public void BuyAttackSpeedUpgrade()
    {
        float myTier1UpgradeAttackSpeed = -0.1f;
        float myTier2UpgradeAttackSpeed = -0.15f;

        if (currentBiomass >= 60 && myFirstAttackSpeedUpgradeBought == true && myAllAttackSpeedUpgradesBought == false)
        {
            myAttackSpeedBonus += myTier2UpgradeAttackSpeed;
            myAllAttackSpeedUpgradesBought = true;
            currentBiomass -= 60;
            Debug.Log("Second Attack Speed Upgrade Bought!");
        }

        if (currentBiomass >= 40 && myFirstAttackSpeedUpgradeBought == false)
        {
            myAttackSpeedBonus += myTier1UpgradeAttackSpeed;
            myFirstAttackSpeedUpgradeBought = true;
            currentBiomass -= 40;
            Debug.Log("First Attack Speed Upgrade Bought!");
        }
        UpdateBiomassText();
    }
    public void BuyRangeUpgrade()
    {
        int myTier1UpgradeRange = 1;
        int myTier2UpgradeRange = 2;

        if (currentBiomass >= 80 && myFirstRangeUpgradeBought == true && myAllRangeUpgradesBought == false)
        {
            myRangeBonus += myTier2UpgradeRange;
            myAllRangeUpgradesBought = true;
            currentBiomass -= 80;
            Debug.Log("Second Range Upgrade Bought!");
        }

        if (currentBiomass >= 60 && myFirstRangeUpgradeBought == false)
        {
            myRangeBonus += myTier1UpgradeRange;
            myFirstRangeUpgradeBought = true;
            currentBiomass -= 60;
            Debug.Log("First Range Upgrade Bought!");
        }
        UpdateBiomassText();
    }

    public void BuyDamageUpgrade()
    {
        int myTier1UpgradeDamage = 1;
        int myTier2UpgradeDamage = 2;

        if (currentBiomass >= 120 && myFirstDamageUpgradeBought == true && myAllDamageUpgradesBought == false)
        {
            myDamageBonus += myTier2UpgradeDamage;
            myAllDamageUpgradesBought = true;
            currentBiomass -= 120;
            Debug.Log("Second Damage Upgrade Bought!");
        }

        if (currentBiomass >= 80 && myFirstDamageUpgradeBought == false)
        {
            myDamageBonus += myTier1UpgradeDamage;
            myFirstDamageUpgradeBought = true;
            currentBiomass -= 80;
            Debug.Log("First Damage Upgrade Bought!");
        }
        UpdateBiomassText();
    }

    public enum EUnlockTurret
    {
        DoublePistolManUnlock,
        SniperManUnlock,
        FlamethrowerManUnlock
    }
}
