using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager sInstance;

    [field: SerializeField] public int currentScrap { get; set; }
    [SerializeField] private TextMeshProUGUI myScrapText;
    List <Tower> myBoughtTowers = new List <Tower> ();
    private Tower myTower;

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
        myScrapText.text = $"{currentScrap}";

        if (true)
        {
            
        }
    }
}
