using UnityEngine;
using System.Collections;

public class Patrol
{
    private Transform zone;
    private int destPoint;
    private int totalPoints;
    public int TotalPoints
    {
        get { return totalPoints; }
        private set { this.totalPoints = value; }
    }

    public Patrol(GameObject _zone)
    {
        this.zone = _zone.GetComponent<Transform>();
        totalPoints = zone.childCount;
        destPoint = 0;
    }



    public Vector3 getPatrolPoint()
    {
        Transform patrolPoint = zone.GetChild(destPoint);
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % totalPoints;

        return patrolPoint.position;
    }

    public Vector3 getPatrolPointReverse()
    {
        Transform patrolPoint = zone.GetChild(totalPoints - destPoint - 1);

        destPoint = (destPoint + 1) % totalPoints;

        return patrolPoint.position;
    }

}
