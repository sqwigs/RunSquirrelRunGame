using UnityEngine;
using System.Collections;
using System;

public class NavigationScript : MonoBehaviour
{
    // movement controls used by Designers
    public float walkRadius;
    public float pauseWait;
    public float moveWait;

    // Nav Mesh Controls
    private NavMeshAgent _navAgent;
    private Vector3 spawnPos; 
    private Vector3 target; // player target found

    private SpriteRenderer sprite; // sprite image of enemy

    // time control
    protected float time;
    protected bool pause;
    protected bool canMove; 
    protected bool targetFound;

    void Start()
    {
        _navAgent = this.GetComponent<NavMeshAgent>();

        sprite = GetComponent<SpriteRenderer>();

        spawnPos = transform.position;


        pause = true;
        canMove = false;
        targetFound = false;
        time += Time.deltaTime;
    }



    // Update is called once per frame
    void Update()
    {
        if (pause)
        {
            if (time > pauseWait)
            {
                pause = !pause;
                time = 0.0f;
                _navAgent.ResetPath();
            }
            time += Time.deltaTime;
        }
        else {
            if (canMove)
            {
                if (targetFound)
                {
                    targMovement();
                }
                else
                {
                    randMovement();
                    pause = !pause;
                    canMove = !canMove;
                }                
            }
            else {
                if (time > moveWait)
                {
                    canMove = !canMove;
                    time = 0.0f;
                }
                time += Time.deltaTime;
            }
        }

        transform.rotation = Quaternion.Euler(270, 0.0f, 0.0f);

    }

    /// <summary>
    /// Finds a random point within the nav mesh within the range given. Returns a boolean if a point was found. 
    /// </summary>
    /// <param name="center"> Starting point for which to find point within Navmesh </param>
    /// <param name="range"> Radius range within the Nav mesh</param>
    /// <param name="result"> Vector to found destination in Nav mesh</param>
    /// <returns> true if point was found, otherwise returns false </returns>
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
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

    /// <summary>
    /// Randomly moves GameObject around NavMesh
    /// </summary>
    void randMovement()
    {
        Vector3 finalPosition;
        if (RandomPoint(spawnPos, walkRadius, out finalPosition))
        {
            move(finalPosition);
        }

    }

    /// <summary>
    /// Moves the GameObject towards the target using NavMesh
    /// </summary>
    void targMovement ()
    {
        move(target);
    }

    /// <summary>
    /// This method takes the finalPosition param and moves the current GameObject to that position in the NavMesh
    /// </summary>
    /// <param name="finalPosition"></param>
    void move (Vector3 finalPosition)
    {
        if (Mathf.Abs(finalPosition.x) - Math.Abs(transform.position.x) > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        finalPosition.y = 0.0f;
        _navAgent.destination = finalPosition;
    }

    /// <summary>
    /// Target was found by detection sphere, and will now move towards that point.
    /// </summary>
    /// <param name="position"></param>
    internal void TargetFound(Vector3 curPos)
    {
        target = curPos;
        targetFound = true; 
    }

    /// <summary>
    /// Target Was Lost and will now move randomly 
    /// </summary>
    internal void TargetLost(Vector3 lastKnownPos)
    {
        target = lastKnownPos;
        targMovement();
        targetFound = false;
    }
}
