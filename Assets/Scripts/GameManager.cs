using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager sInstance;

    [field: SerializeField] public int currentScrap { get; set; }
    [SerializeField] private TextMeshProUGUI myScrapText;
    [SerializeField] private TextMeshProUGUI myNextRound;
    [SerializeField] private TextMeshProUGUI myWeakness;
    [SerializeField] private TextMeshProUGUI myRoundDuration;
    [SerializeField] private ETowerType myTowerType;
    [SerializeField] List <ETowerType> myBoughtTowers = new List <ETowerType> ();

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

    public void TowerType(int value)
    {
        switch (value) 
        {
            case (int)ETowerType.PistolMan:
                myBoughtTowers.Add(ETowerType.PistolMan);
                break;

            case (int)ETowerType.DoublePistolMan:
                myBoughtTowers.Add(ETowerType.DoublePistolMan);
                break;

            case (int)ETowerType.SniperMan:
                myBoughtTowers.Add(ETowerType.SniperMan);
                break;

            case (int)ETowerType.FlamethrowerMan:
                myBoughtTowers.Add(ETowerType.FlamethrowerMan);
                break;

            default:
                myBoughtTowers.Add(ETowerType.Null);
                break;
        }
    }
}


