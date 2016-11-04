using UnityEngine;
using System.Collections;

public class Patrol
{
    private Transform zone;
    private int destPoint = 0;
    private int totalPoints;

    //void Start()
    //{
    //    totalPoints = transform.childCount;
    //}

    public Patrol (GameObject _zone)
    {
        this.zone = _zone.GetComponent<Transform>();
        totalPoints = zone.childCount;
    }

    public int totalPatrolDest()
    {
        return totalPoints;
    }

    public Vector3 getPatrolPoint()
    {
        Transform patrolPoint = zone.GetChild(destPoint); ;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % totalPoints;

        return patrolPoint.position;
    }
}
