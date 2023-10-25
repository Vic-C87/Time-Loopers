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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            myParent.EnemyEnteredTrigger(other.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            myParent.EnemyExitedTrigger(other.gameObject.GetComponent<Enemy>());
        }
    }
}
