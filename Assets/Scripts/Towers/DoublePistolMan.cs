using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoublePistolMan : Tower
{
    public GameObject BulletPrefab;

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
    private void Attack()
    {
        if (Time.realtimeSinceStartup - myLastAttackTime >= myAttackRate)
        {
            myLastAttackTime = Time.realtimeSinceStartup;

            int chosenEnemy = Random.Range(0, myPossibleTargets.Count);

            transform.LookAt(myPossibleTargets[chosenEnemy].transform);

            Debug.Log($"{chosenEnemy}");

            GameObject PistolBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            PistolBullet.GetComponent<Bullets>().ShootAt(myPossibleTargets[chosenEnemy]);
            Debug.Log("Attacking");

        }
    }
}
