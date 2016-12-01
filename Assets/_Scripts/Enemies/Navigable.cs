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

    // Mesh Animation Controls
    protected GameObject runningAnimu;
    protected GameObject idleAnimu;

    /// <summary>
    /// Establish base for navigation variables used by child classes. 
    /// 
    /// If the Start function doesn't fit the needs of the child, feel 
    /// free to override. 
    /// </summary>
    public virtual void Start()
    {
        _navAgent = this.GetComponent<NavMeshAgent>();

        // inherited agents will auto rotate along nav mesh vector
        _navAgent.updateRotation = true;

        // get the animator game object to handle
        if (!GetChild(this.gameObject, "Running", out runningAnimu))
        {
            Debug.Log("Could not find child \"Running\" of player object");
        }

        // get the animator game object to handle
        if (!GetChild(this.gameObject, "Idle", out idleAnimu))
        {
            Debug.Log("Could not find child \"Idle\" of player object");
        }

        sprite = GetComponent<SpriteRenderer>();

        spawnPos = transform.position;

        pause = false;
        canMove = true;
        targetFound = false;
        time += Time.deltaTime;
    }

    protected void Update()
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
    protected virtual void patrolMovement()
    {
        randomPatrolMovement();
    }

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
        targetFound = true;
    }

    /// <summary>
    /// Target Was Lost and will now move accordingly to patrol 
    /// </summary>
    public virtual void TargetLost(Vector3 lastKnownPos)
    {
        target = lastKnownPos;
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


    /// <summary>
    /// Gets the child gameObject whose name is specified by 'wanted'
    /// The search is non-recursive by default unless true is passed to 'recursive'
    /// 
    /// Will return bool if child was found and place that child in childObject out param.
    /// 
    /// ********************* USED FROM THE FOLLOWING SOURCE *************************
    /// http://answers.unity3d.com/questions/726780/disabling-child-gameobject-from-script-attached-to.html
    /// 
    /// ******************************************************************************
    /// 
    /// </summary>
    protected bool GetChild(GameObject inside, string wanted, out GameObject childObject, bool recursive = false)
    {
        childObject = null;
        foreach (Transform child in inside.transform)
        {
            if (child.name == wanted)
            {
                childObject = child.gameObject;
                return true;
            }
            if (recursive)
            {
                var within = GetChild(child.gameObject, wanted, out childObject, true);
                if (within) return within;
            }
        }
        return false;
    }

    /// <summary>
    /// Base Random Patrol Movement
    /// </summary>
    protected void randomPatrolMovement()
    {
        if (pause)
        {
            if (time > movementTime || _navAgent.remainingDistance == 0) // how long to move for
            {
                pause = !pause;
                time = 0.0f;
                _navAgent.ResetPath();
                runningAnimu.SetActive(false);
                idleAnimu.SetActive(true);
            }
            else
            {
                time += Time.deltaTime;
                
            }
           
        }
        else {
            if (canMove)
            {
                runningAnimu.SetActive(true);
                idleAnimu.SetActive(false);
                randMovement();
                pause = !pause;
                canMove = !canMove;
            }
            else {
                if (time > waitTime) // wait to move again
                {
                    canMove = !canMove;
                    time = 0.0f;
                }
                time += Time.deltaTime;
            }
        }

        
    }

    /// <summary>
    /// Randomly moves GameObject around NavMesh
    /// </summary>
    private void randMovement()
    {
        Vector3 finalPosition;
        if (RandomPoint(spawnPos, walkRadius, out finalPosition))
        {
            finalPosition.y = 0.0f;
            _navAgent.destination = finalPosition;
        }

    }
}
