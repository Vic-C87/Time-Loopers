using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float myAttackRate;
    [SerializeField] protected Vector3 mySize;
    [SerializeField] protected Vector3 myPosition;
    [SerializeField] protected EAmmoType myAmmo;
}

public enum EAmmoType
{
    none
}