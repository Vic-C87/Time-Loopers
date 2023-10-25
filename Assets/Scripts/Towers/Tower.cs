using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected GameObject myBulletPrefab;

    [SerializeField] protected float myAttackRate;
    [SerializeField] protected int myLevel;
    [SerializeField] protected Vector3 mySize;
    [SerializeField] protected Vector3 myPosition;
    [SerializeField] protected EAmmoType myAmmo;
    protected List<GameObject> myPossibleTargets;
    protected float myLastAttackTime;

    public void EnemyEnteredTrigger(GameObject aTarget)
    {
        myPossibleTargets.Add(aTarget);
    }

    public void EnemyExitedTrigger(GameObject aTarget)
    {
        myPossibleTargets.Remove(aTarget);
    }

    protected virtual void Attack()
    {
        if (Time.realtimeSinceStartup - myLastAttackTime >= myAttackRate)
        {
            myLastAttackTime = Time.realtimeSinceStartup;

            int chosenEnemy = Random.Range(0, myPossibleTargets.Count);

            Vector3 aLookAtPosition = myPossibleTargets[chosenEnemy].transform.position;
            aLookAtPosition.y = transform.position.y;

            transform.LookAt(aLookAtPosition);

            Debug.Log(chosenEnemy);

            GameObject pistolBullet = Instantiate(myBulletPrefab, transform.position, Quaternion.identity);
            pistolBullet.GetComponent<Bullets>().ShootAt(myPossibleTargets[chosenEnemy]);
            Debug.Log("Attacking");

        }
    }
}

public enum EAmmoType
{
    Normal,
    Fire,
    Pierce
}