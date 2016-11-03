using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Navigating code for coyote enemy
/// 
/// Patrol Code taken from : https://docs.unity3d.com/Manual/nav-AgentPatrol.html
/// 
/// </summary>

public class CoyoteNavigation : Navigable {

    public GameObject zoneObject;
    private Patrol patrol;

    public override void Start()
    {
        base.Start();

        if (zoneObject != null)
        {
            patrol = new Patrol(zoneObject) ;
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
}
