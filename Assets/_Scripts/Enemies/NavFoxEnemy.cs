using UnityEngine;
using System.Collections;
using System;

public class NavFoxEnemy : Navigable
{

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

    }

    /// <summary>
    /// Randomly moves GameObject around NavMesh
    /// </summary>
    void randMovement()
    {
        Vector3 finalPosition;
        if (RandomPoint(spawnPos, walkRadius, out finalPosition))
        {
            finalPosition.y = transform.position.y;
            _navAgent.destination = finalPosition;
            _navAgent.updateRotation = true;
        }

    }

    protected override IEnumerator FreezeInPlace()
    {
        yield return null;
    }


}
