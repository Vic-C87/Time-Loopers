using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolMan : Tower
{
    private GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject myBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
        }
    }
}
