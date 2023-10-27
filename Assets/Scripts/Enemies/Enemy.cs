using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int myLevel;                                   
    [SerializeField] float myMaxHP;        
    float myHP;                                    
    [SerializeField] int myScrapValue;             
    [SerializeField] int myZoneMultiplier;         
    [SerializeField] float myDamage;               
    float myAttackRate;                            
    bool myIsAttacking;                            
    float myLastHit;                               
    Seeker mySeeker;              
    Animator myAnimator;
    Rigidbody myBody;
    bool myIsTakingPlasmaDamage = false;

    float myPlasmaDamageTaken;

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
        myPlasmaDamageTaken = 15f;
        //Make call to static GameManager to set myPlasmaDamageTaken
    }

    void FixedUpdate()
    {
        if (myIsTakingPlasmaDamage) 
        {
            TakePlasmaDamage();
            myIsTakingPlasmaDamage = false;
            Debug.Log(myHP + " HP left");
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

    void TakePlasmaDamage()
    {
        myHP -= myPlasmaDamageTaken * Time.fixedDeltaTime;
        if (myHP <= 0)
        {
            myHP = 0;
            Die();
        }
    }

    private void Die()
    {
        GameManager.sInstance.LevelEarnings += myScrapValue;
        Destroy(this.gameObject);
    }
    public float DoDamage()
    {
        return myDamage;
    }

    public float Explode()
    {
        //Instantiate Explosion
        Destroy(this.gameObject, 0.2f);
        return myDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Zone"))
        {
            var zone = other.GetComponent<Zone>();            
            myZoneMultiplier = zone.GetMultiplier();
        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<ParticleSystem>().GetComponentInParent<Tower>())
            myIsTakingPlasmaDamage = true;
    }

    
}
