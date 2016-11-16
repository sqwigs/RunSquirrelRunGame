using UnityEngine;
using System.Collections;
using System;

public class WolfNavigation : Navigable {

    public GameObject zoneObject;
    private Patrol patrol;

    public override void Start()
    {
        base.Start();

        if (zoneObject != null)
        {
            patrol = new Patrol(zoneObject);
        }
        else
        {
            Debug.Log("Cannot have a null zone object!");
        }

        _navAgent.autoBraking = false;
        _navAgent.destination = patrol.getPatrolPoint();
    }

    /// <summary>
    /// Move Coyote around edges of nav mesh. 
    /// </summary>
    protected override void patrolMovement()
    {
        if (_navAgent.remainingDistance < 0.5f)
        {
            // Returns if no points have been set up
            if (patrol.totalPatrolDest() == 0)
                return;
            // Set the agent to go to the currently selected destination.
            _navAgent.destination = patrol.getPatrolPoint();

          
        }
    }

    public override void TargetFound(Vector3 lastKnownPos)
    {
        if (!targetFound )
            _navAgent.speed *= 2;

        base.TargetFound(lastKnownPos);   
    }

    public override void TargetLost (Vector3 lastKnownPos)
    {
        if (targetFound)
            _navAgent.speed /= 2;

        base.TargetLost(lastKnownPos);
    }

    protected override IEnumerator FreezeInPlace()
    {
        _navAgent.Stop();

        yield return new WaitForSeconds(timeFrozen);

        _navAgent.Resume();
    }
}
