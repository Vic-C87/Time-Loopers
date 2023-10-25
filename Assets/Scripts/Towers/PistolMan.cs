using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PistolMan : Tower
{
    [SerializeField] GameObject BulletPrefab;

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

            Vector3 aLookAtPosition = myPossibleTargets[chosenEnemy].transform.position;
            aLookAtPosition.y = transform.position.y;

            transform.LookAt(aLookAtPosition);

            Debug.Log(chosenEnemy);

            GameObject pistolBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            pistolBullet.GetComponent<Bullets>().ShootAt(myPossibleTargets[chosenEnemy]);
            Debug.Log("Attacking");

        }
    }
}
