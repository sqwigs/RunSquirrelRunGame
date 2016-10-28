using UnityEngine;
using System.Collections;
using System;

public abstract class NavigationInterface : MonoBehaviour
{
    // movement controls used by Designers
    public float walkRadius;
    public float pauseWait;
    public float moveWait;

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


        pause = true;
        canMove = false;
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

        transform.rotation = Quaternion.Euler(270, 0.0f, 0.0f);
    }

    /// <summary>
    /// Character moves along path determined through inheritence 
    /// </summary>
    protected abstract void patrolMovement() ;

    /// <summary>
    /// character uses Navmesh to move towards target. 
    /// </summary>
    /// <param name="targetPosition"></param>
    protected virtual void targetedMovement()
    {
        _navAgent.destination = target;
    }

    /// <summary>
    /// Target was found by detection sphere, and will now move towards that point.
    /// </summary>
    /// <param name="position"></param>
    public void TargetFound(Vector3 curPos)
    {
        target = curPos;
        target.y = 0.0f;
        targetFound = true;
    }

    /// <summary>
    /// Target Was Lost and will now move accordingly to patrol 
    /// </summary>
    public void TargetLost(Vector3 lastKnownPos)
    {
        target = lastKnownPos;
        target.y = 0.0f;
        patrolMovement();
        targetFound = false;
    }

}
