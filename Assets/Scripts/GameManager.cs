using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager sInstance;

    Queue<Seeker> myEnemies = new Queue<Seeker>();
    List<Seeker> myInactives = new List<Seeker>();
    public bool IsFindingPath = false;
    float myCurrentLevelEarnings = 0;

    void Awake()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            sInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RequestNewPath();
    }

    public void DequeueMe(Seeker aSeeker)
    {
        if (!myInactives.Contains(aSeeker))
        {
            myInactives.Add(aSeeker);
        }

    }

    public void AddNewPathSeeker(Seeker aSeeker)
    {
        if (!myEnemies.Contains(aSeeker))
        {
            if (myInactives.Contains(aSeeker))
                myInactives.Remove(aSeeker);
            myEnemies.Enqueue(aSeeker);
        }
    }

    public void RequestNewPath()
    {
        if (/*!myIsFindingPath && */myEnemies.Count > 0)
        {
            Seeker seeker = myEnemies.Dequeue();
            if (seeker != null)
            {
                if (!myInactives.Contains(seeker))
                    seeker.Seek();
                IsFindingPath = true;
            }
        }
    }

    public bool CheckIfActiveSeeker(Seeker aSeeker)
    {
        return !myInactives.Contains(aSeeker);
    }

    public void AddLevelEarnings(float aSum)
    {
        myCurrentLevelEarnings += aSum;
    }

    public float GetLevelEarnings()
    {
        float levelEarnings = myCurrentLevelEarnings;
        myCurrentLevelEarnings = 0;
        return levelEarnings;
    }
}
