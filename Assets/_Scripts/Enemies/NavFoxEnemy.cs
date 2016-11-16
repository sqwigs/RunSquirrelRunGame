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

    protected override IEnumerator FreezeInPlace()
    {
        yield return null;
    }


}
