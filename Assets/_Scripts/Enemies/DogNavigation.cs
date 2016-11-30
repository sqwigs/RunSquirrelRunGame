using UnityEngine;
using System.Collections;
using System;

public class DogNavigation : Navigable
{
    protected override void patrolMovement()
    {
        if (pause)
        {
            if (time > movementTime) // how long to move for
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
    void randMovement()
    {
        Vector3 finalPosition;
        if (RandomPoint(spawnPos, walkRadius, out finalPosition))
        {
            finalPosition.y = 0.0f;
            _navAgent.destination = finalPosition;
        }

    }

    protected override IEnumerator FreezeInPlace()
    {
        _navAgent.Stop();

        yield return new WaitForSeconds(timeFrozen);

        _navAgent.Resume();
    }
}
