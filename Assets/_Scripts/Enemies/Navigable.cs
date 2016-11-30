using UnityEngine;
using System.Collections;
using System;

public abstract class Navigable : MonoBehaviour
{
    // movement controls used by Designers
    public float walkRadius;
    public float movementTime;
    public float waitTime;

    // Nav Mesh Controls
    protected NavMeshAgent _navAgent;
    protected Vector3 spawnPos;
    protected Vector3 target; // player target found

    protected SpriteRenderer sprite; // sprite image of enemy

    // time control
    protected float time;
    protected bool pause;
    protected bool canMove;
    protected bool targetFound;

    // freeze control
    public float timeFrozen;

    /// <summary>
    /// Establish base for navigation variables used by child classes. 
    /// 
    /// If the Start function doesn't fit the needs of the child, feel 
    /// free to override. 
    /// </summary>
    public virtual void Start()
    {
        _navAgent = this.GetComponent<NavMeshAgent>();

        sprite = GetComponent<SpriteRenderer>();

        spawnPos = transform.position;

        _navAgent.updateRotation = false;

        pause = false;
        canMove = true;
        targetFound = false;
        time += Time.deltaTime;
    }

    void Update ()
    {
        if (targetFound)
        {
            targetedMovement();
        }
        else
        {
            patrolMovement();
        }
    }

    /// <summary>
    /// Character moves along path determined through inheritence 
    /// </summary>
    protected abstract void patrolMovement() ;

    /// <summary>
    /// Character is frozen based on implmentation of inherited members
    /// </summary>
    internal virtual void Freeze()
    {
        StartCoroutine(FreezeInPlace());
    }

    /// <summary>
    /// character uses Navmesh to move towards target. 
    /// </summary>
    /// <param name="targetPosition"></param>
    protected void targetedMovement()
    {
        _navAgent.destination = target;
    }

    /// <summary>
    /// Target was found by detection sphere, and will now move towards that point.
    /// </summary>
    /// <param name="position"></param>
    public virtual void TargetFound(Vector3 curPos)
    {
        target = curPos;
        //target.y = 0.0f;
        targetFound = true;
    }

    /// <summary>
    /// Target Was Lost and will now move accordingly to patrol 
    /// </summary>
    public virtual void TargetLost(Vector3 lastKnownPos)
    {
        target = lastKnownPos;
        //target.y = 0.0f;
        patrolMovement();
        targetFound = false;
    }

    protected abstract IEnumerator FreezeInPlace();

    /// <summary>
    /// Finds a random point within the nav mesh within the range given. Returns a boolean if a point was found. 
    /// </summary>
    /// <param name="center"> Starting point for which to find point within Navmesh </param>
    /// <param name="range"> Radius range within the Nav mesh</param>
    /// <param name="result"> Vector to found destination in Nav mesh</param>
    /// <returns> true if point was found, otherwise returns false </returns>
    protected bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            //randomPoint.y = 0.0f;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
