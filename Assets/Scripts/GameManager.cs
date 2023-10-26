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
    List <Tower> myBoughtTowers = new List <Tower> ();

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

    public static ETowerType TowerType(ETowerType towerType)
    {
        switch (towerType) 
        {
            case ETowerType.PistolMan:
                return ETowerType.PistolMan;

            case ETowerType.DoublePistolMan:
                return ETowerType.DoublePistolMan;

            case ETowerType.SniperMan:
                return ETowerType.SniperMan;

            case ETowerType.FlamethrowerMan:
                return ETowerType.FlamethrowerMan;
            
            default: 
                return ETowerType.Null;         
        }
    }
}


