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
        Vector3 target = aTarget.transform.position - transform.position;
        target.y -= .5f; 
        myBody.AddForce((target).normalized * mySpeed, ForceMode.Impulse);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(myDamage, this);
            Destroy(gameObject);
        }
    }
}
