using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PistolMan : Tower
{
    public GameObject Bullet;
    void Update()
    {
        //on space shoot bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bull = Instantiate(Bullet, transform.position, Quaternion.identity);
        }
    }
}
