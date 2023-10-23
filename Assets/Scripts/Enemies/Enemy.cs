using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int myLevel;
    protected float myMaxHP;
    protected float myHP;
    protected int myScrapValue;
    protected float myDamage;
    protected float myAttackRate;           //seconds per hit
    protected bool myIsAttacking;
    protected float myLastHit;              //time since last hit
    protected Seeker mySeeker;


    // Start is called before the first frame update
    void Awake()
    {
        mySeeker = GetComponent<Seeker>();
        myLastHit = Time.realtimeSinceStartup;
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
}
