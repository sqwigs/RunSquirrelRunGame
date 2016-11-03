using UnityEngine;
using System.Collections;
using System;

public class DogNavigation : Navigable
{
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
            randomPoint.y = 0.0f;
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

    protected override void patrolMovement()
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
                    randMovement();
                    pause = !pause;
                    canMove = !canMove;
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
    /// Randomly moves GameObject around NavMesh
    /// </summary>
    void randMovement()
    {
        Vector3 finalPosition;
        if (RandomPoint(spawnPos, walkRadius, out finalPosition))
        {
            finalPosition.y = 0.0f;
            _navAgent.destination = finalPosition;
        }

    }

}
