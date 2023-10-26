using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int myLevel;                                  // level of enemy
    [SerializeField] protected float myMaxHP = 1000;        // maximum hp 
    protected float myHP;                                   // current hp
    [SerializeField] protected int myScrapValue;                             // base value when killed
    [SerializeField] protected int myZoneMultiplier;         // multiplier for value based on zone
    protected float myDamage;                               // damage output
    protected float myAttackRate;                           // seconds per hit
    protected bool myIsAttacking;                           // whether attacking or not
    protected float myLastHit;                              // time since last hit
    protected Seeker mySeeker;              
    protected Animator myAnimator;
    protected Rigidbody myBody;

    // Start is called before the first frame update
    void Awake()
    {
        myZoneMultiplier = 1;
        myHP = myMaxHP;
        myLastHit = Time.realtimeSinceStartup;
        mySeeker = GetComponent<Seeker>();
        myAnimator = GetComponentInChildren<Animator>();
        myBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myIsAttacking)
        {       
            if ((Time.realtimeSinceStartup - myLastHit) > myAttackRate)
            {
                DoDamage();
                myLastHit = Time.realtimeSinceStartup;
            }     
        }
    }
    
    public void TakeDamage(float someDamage, Bullets aBullet)
    {
        myHP -= someDamage;
        if (myHP <= 0)
        {
            myHP = 0;
            Die();
        }
        myBody.AddForce(aBullet.transform.up * someDamage, ForceMode.Impulse);
    }

    private void Die()
    {
        BattleManager.sInstance.AddLevelEarnings(myZoneMultiplier * myScrapValue);
        Destroy(this.gameObject);
    }
    public float DoDamage()
    {
        return myDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Attacking");
            myIsAttacking = true;
            mySeeker.StopFollow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Zone"))
        {
            var zone = other.GetComponent<Zone>();            
            myZoneMultiplier = zone.GetMultiplier();
        }
        
    }
}
