using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomQuack : MonoBehaviour
{
    float myLifeTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, myLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
