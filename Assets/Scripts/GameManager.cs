using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager sInstance;

    [field: SerializeField] public int currentScrap = 100;
    [SerializeField] private TextMeshProUGUI myScrapText;
    [SerializeField] private TextMeshProUGUI myNextRound;
    [SerializeField] private TextMeshProUGUI myWeakness;
    [SerializeField] private TextMeshProUGUI myRoundDuration;
    [SerializeField] private ETowerType myTowerType;
    [SerializeField] List<ETowerType> myBoughtTowers = new List<ETowerType>();

    private void Awake()
    {
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
        myScrapText.text = $"{currentScrap} Scrap";

    }

    public void AddTower(int value)
    {
        switch (value)
        {
            case (int)ETowerType.PistolMan:
                if (currentScrap >= 10)
                {
                    myBoughtTowers.Add(ETowerType.PistolMan);
                    currentScrap -= 10;
                }
                else
                {
                    Debug.Log("Not enough cash");
                }
                break;

            case (int)ETowerType.DoublePistolMan:
                if (currentScrap >= 50)
                {
                    myBoughtTowers.Add(ETowerType.DoublePistolMan);
                    currentScrap -= 50;
                }
                else
                {
                    Debug.Log("Not enough cash");
                }
                break;

            case (int)ETowerType.SniperMan:
                if (currentScrap >= 70)
                {
                    myBoughtTowers.Add(ETowerType.SniperMan);
                    currentScrap -= 70;
                }
                else
                {
                    Debug.Log("Not enough cash");
                }
                break;

            case (int)ETowerType.FlamethrowerMan:
                if (currentScrap >= 100)
                {
                    myBoughtTowers.Add(ETowerType.FlamethrowerMan);
                    currentScrap -= 100;
                }
                else
                {
                    Debug.Log("Not enough cash");
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
                    currentScrap += 5;
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
                    currentScrap += 25;
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
                    currentScrap += 35;
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
                    currentScrap += 50;
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
}


