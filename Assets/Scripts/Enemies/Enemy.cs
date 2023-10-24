using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int myLevel;                  // level of enemy
    protected float myMaxHP;                // maximum hp 
    protected float myHP;                   // current hp
    protected int myScrapValue;             // base value when killed
    [SerializeField]protected int myZoneMultiplier;         // multiplier for value based on zone
    protected float myDamage;               // damage output
    protected float myAttackRate;           // seconds per hit
    protected bool myIsAttacking;           // whether attacking or not
    protected float myLastHit;              // time since last hit
    protected Seeker mySeeker;              
    protected Animator myAnimator;  

    // Start is called before the first frame update
    void Awake()
    {
        myZoneMultiplier = 1;
        myLastHit = Time.realtimeSinceStartup;
        mySeeker = GetComponent<Seeker>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myIsAttacking)
        {       
            if (Time.realtimeSinceStartup - myLastHit > myAttackRate)
            {
                DoDamage();
                myLastHit = Time.realtimeSinceStartup;
            }     
        }
    }
    
    public void TakeDamage(int someDamage)
    {
        myHP -= someDamage;
        if (myHP <= 0)
        {
            myHP = 0;
            Die();
        }
    }

    private void Die()
    {
       
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
        if(other.CompareTag("Player"))
        {
            var zone = other.GetComponent<Zone>();            
            myZoneMultiplier = zone.GetMultiplier();
        }
        
    }
}
