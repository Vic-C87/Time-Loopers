using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerCheck : MonoBehaviour
{
    Tower myParent;

    void Awake()
    {
        myParent = GetComponentInParent<Tower>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            myParent.EnemyEnteredTrigger(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            myParent.EnemyExitedTrigger(other.gameObject);
        }
    }
}
