using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public Transform Target;
    [SerializeField]
    float mySpeed = 5f;
    Vector3[] myPath;
    int myTargetIndex;
    [SerializeField]
    float myAcceptableStopDistance;

    void Start()
    {
        // TODO
        //Get transform of target
        OnSpawn();
    }

    public void OnSpawn()
    {
        if (Target != null)
            Seek();
    }

    public void Seek()
    {
        PathManager.RequestPath(transform.position, Target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] aNewPath, bool aSuccessMessage)
    {
        if (aSuccessMessage)
        {
            myPath = aNewPath;
            myTargetIndex = 0;
            StopCoroutine("FollowPath");
            if (this.gameObject.activeSelf)
            {
                StartCoroutine("FollowPath");
            }
        }
    }

    public void StopFollow()
    {
        myPath = null;
        myTargetIndex = 0;
        StopCoroutine("FollowPath");
    }

    IEnumerator FollowPath()
    {
        if (myPath.Length == 0)
        {
            yield break;
        }
        Vector3 currentWaypoint = myPath[0];
        while (true)
        {
            if (Vector3.Distance(transform.position, currentWaypoint) < myAcceptableStopDistance)
            {
                myTargetIndex++;
                if (myTargetIndex >= myPath.Length)
                {
                    yield break;
                }
                currentWaypoint = myPath[myTargetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, mySpeed * Time.deltaTime);
            transform.LookAt(currentWaypoint);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (myPath != null)
        {
            for (int i = myTargetIndex; i < myPath.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(myPath[i], Vector3.one);

                if (i == myTargetIndex)
                {
                    Gizmos.DrawLine(transform.position, myPath[i]);
                }
                else
                {
                    Gizmos.DrawLine(myPath[i - 1], myPath[i]);
                }
            }
        }
    }
}