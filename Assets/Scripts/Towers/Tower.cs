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

    void Awake()
    {
        myPossibleTargets = new List<Enemy>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        myLastAttackTime = Time.realtimeSinceStartup;
        
    }

    void Update()
    {
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
            Attack();
        }
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
        Quaternion oldRotation = transform.rotation;            
        transform.LookAt(myCurrentTarget.transform);
        Quaternion newRotation = transform.rotation;
        transform.rotation = oldRotation;
        if (transform.rotation.eulerAngles.y <= newRotation.eulerAngles.y + 1f && transform.rotation.eulerAngles.y >= newRotation.eulerAngles.y - 1f)
        {
            myIsFacingTarget = true;
        }

        Quaternion currentRotation = Quaternion.Slerp(transform.rotation, newRotation, myTurnSpeed * Time.deltaTime);
        currentRotation.eulerAngles = new Vector3 (0, currentRotation.eulerAngles.y, 0);
        transform.rotation = currentRotation;
    }

    void Attack()
    {
        if (Time.realtimeSinceStartup - myLastAttackTime >= myAttackRate)
        {
            myLastAttackTime = Time.realtimeSinceStartup;

            GameObject pistolBullet = Instantiate(myBulletPrefab, transform.position + (transform.forward * 0.1f) + (transform.up * .2f), Quaternion.identity);
            SetBulletRotation(pistolBullet);
            myAnimationController.SetBool("myShootingAnimation", true);
            pistolBullet.GetComponent<Bullets>().ShootAt(myCurrentTarget.gameObject);
            Debug.Log("Attacking " + myCurrentTarget);
            ResetTarget(myCurrentTarget);
            myAudioSource.Play();
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