using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMan : Tower
{
    private void Awake()
    {
        myLastAttackTime = Time.realtimeSinceStartup;
        myPossibleTargets = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myPossibleTargets.Count > 0)
        {
            Attack();
        }
    }
}
