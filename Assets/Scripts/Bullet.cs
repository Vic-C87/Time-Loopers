using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Bullets
{
    [SerializeField] float mySpeed;
    [SerializeField] float myLifeSpan;
    [SerializeField] bool myAreaDamage;
    [SerializeField] Vector3 myAreaOfEffect;
    Rigidbody myBody;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, myLifeSpan);
    }

    public override void ShootAt(GameObject aTarget)
    {
        myBody.AddForce((aTarget.transform.position - transform.position).normalized * mySpeed, ForceMode.Impulse);

    }
}
