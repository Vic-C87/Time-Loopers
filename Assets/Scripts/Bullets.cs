using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] float myDamage;
    [SerializeField] float mySpeed;
    [SerializeField] float myLifeSpan;
    [SerializeField] bool myAreaDamage;
    [SerializeField] Vector3 myAreaOfEffect;
    Rigidbody myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, myLifeSpan);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootAt(GameObject aTarget) 
    { 
        myBody.AddForce((aTarget.transform.position - transform.position).normalized * mySpeed, ForceMode.Impulse);
    }
}
