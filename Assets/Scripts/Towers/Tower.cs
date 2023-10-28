using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject myBulletPrefab;
    [SerializeField] float myTurnSpeed;
    [SerializeField] float myAttackRate;
    [SerializeField] int myLevel;
    [SerializeField] Vector3 mySize;
    [SerializeField] Vector3 myPosition;
    [SerializeField] EAmmoType myAmmo;
    [SerializeField] ETowerType myTowerType;
    [SerializeField] EClipName mySoundEffect;
    [SerializeField] Animator myAnimationController;
    [SerializeField] int myCost;
    List<Enemy> myPossibleTargets;
    float myLastAttackTime;
    bool myIsFacingTarget = false;
    bool myHaveTarget = false;

    Enemy myCurrentTarget;
    AudioSource myAudioSource;
    [SerializeField] ParticleSystem myParticleSystem;
    

    void Awake()
    {
        myPossibleTargets = new List<Enemy>();
        myAudioSource = GetComponent<AudioSource>();
        myAnimationController = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        myLastAttackTime = Time.realtimeSinceStartup;

        if (SoundManager.sInstance.GetAudioClip(mySoundEffect, out AudioClip aClip)) 
        {
            myAudioSource.clip = aClip;
        }
    }

    void Update()
    {
        if (myTowerType == ETowerType.FlamethrowerMan && Time.realtimeSinceStartup - myLastAttackTime >= myAttackRate)
        {
            myParticleSystem.Stop();
        }
        if (!myHaveTarget)
        {
            GetTarget();
        }
        else if (!myIsFacingTarget) 
        {
            FaceTarget();
        }
        else
        {
            if (myCurrentTarget !=null)
            {
                Attack();
            }
            else
            {
                ResetTarget(myCurrentTarget);
            }
        }
    }

    public ETowerType GetTowerType() 
    {
        return myTowerType;
    }

    public void EnemyEnteredTrigger(Enemy aTarget)
    {
        myPossibleTargets.Add(aTarget);
    }

    public void EnemyExitedTrigger(Enemy aTarget)
    {
        ResetTarget(aTarget);
        myPossibleTargets.Remove(aTarget);

        if (myPossibleTargets.Count == 0)
        {
            myAnimationController.SetBool("myShootingAnimation", false);
        }
    }

    void GetTarget()
    {
        if (myPossibleTargets.Count > 0)
        {
            int chosenEnemy = Random.Range(0, myPossibleTargets.Count);
            myCurrentTarget = myPossibleTargets[chosenEnemy];
            myHaveTarget = true;
            myIsFacingTarget = false;
        }
    }

    void ResetTarget(Enemy aTarget)
    {
        if (aTarget == myCurrentTarget)
        {
            myHaveTarget = false;
            myIsFacingTarget = false;
            myCurrentTarget = null;
            
        }
    }

    void FaceTarget()
    {
        if (myCurrentTarget !=  null) 
        {
            myAnimationController.SetBool("myShootingAnimation", false);
            Quaternion newRotation = Quaternion.FromToRotation(transform.position, myCurrentTarget.transform.position);//transform.rotation;
            if (transform.rotation.eulerAngles.y <= newRotation.eulerAngles.y + 1f && transform.rotation.eulerAngles.y >= newRotation.eulerAngles.y - 1f)
            {
                myIsFacingTarget = true;
            }
            
            Quaternion currentRotation = Quaternion.Slerp(transform.rotation, newRotation, myTurnSpeed * Time.deltaTime);
            currentRotation.eulerAngles = new Vector3 (0, currentRotation.eulerAngles.y, 0);
            transform.rotation = currentRotation;
        }
        else
        {
            ResetTarget(myCurrentTarget);
        }
    }

    void Attack()
    {
        if (Time.realtimeSinceStartup - myLastAttackTime >= myAttackRate)
        {
            myLastAttackTime = Time.realtimeSinceStartup;
            if (myTowerType == ETowerType.FlamethrowerMan)
            {
                myParticleSystem.Play();
                ResetTarget(myCurrentTarget);
                myAudioSource.Play();
            }
            else
            {

                GameObject bullet = Instantiate(myBulletPrefab, transform.position + (transform.forward * 0.1f) + (transform.up * .2f), Quaternion.identity);
                SetBulletRotation(bullet);
                myAnimationController.SetBool("myShootingAnimation", true);
                bullet.GetComponent<Bullets>().ShootAt(myCurrentTarget.gameObject);
                Debug.Log("Attacking " + myCurrentTarget);
                ResetTarget(myCurrentTarget);
                myAudioSource.Play();
            }
        }
    }

    void SetBulletRotation(GameObject aBullet)
    {
        Quaternion bulletRotation = new Quaternion();
        bulletRotation.eulerAngles = new Vector3(90f, 0f, 0f);
        bulletRotation.eulerAngles = transform.rotation.eulerAngles + bulletRotation.eulerAngles;
        aBullet.transform.localRotation = bulletRotation;
    }

    public int GetCost()
    {
        return myCost;
    }
}

public enum EAmmoType
{
    Normal,
    Fire,
    Pierce
}

public enum ETowerType
{
    PistolMan,
    DoublePistolMan,
    SniperMan,
    FlamethrowerMan,
    Null
}