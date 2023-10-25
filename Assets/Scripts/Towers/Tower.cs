using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
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
}

public enum EAmmoType
{
    Normal,
    Fire,
    Pierce
}