using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class FoxNavigation : EnemyInterface
{

    // time control
    protected float time;
    protected bool wait;

    void Start()
    {
        transform = GetComponent<Transform>();
        destination = new Vector3(0, 0, 0);
        speed /= 100;
    }

    void Update()
    {
        if (!wait)
        {
            movement();
        }
        else
        {
            if (time > pauseWait)
            {
                wait = !wait;
                time = 0.0f;
            }
            time += Time.deltaTime;
        }
    }

    /// <summary>
    /// Produces random movement for the enemy 
    /// within the boundary determined by user
    /// </summary>
    protected override void movement()
    {
            // ***** Taken From the Following Website *******
            // http://answers.unity3d.com/questions/336663/random-movement-staying-in-an-area.html
            // 
            // ***** I DO NOT TAKE CREDIT FOR THIS WORK *****
            // **********************************************

            // ************************************* START OF COPIED WORK ****************************************

            time += Time.deltaTime;

            if (transform.localPosition.x > boundary.xMax)
            {
                destination.x = UnityEngine.Random.Range(-speed, 0.0f);

                time = 0.0f;
                wait = !wait;
            }
            if (transform.localPosition.x < boundary.xMin)
            {
                destination.x = UnityEngine.Random.Range(0.0f, speed);

                time = 0.0f;
                wait = !wait;
            }
            if (transform.localPosition.z > boundary.zMax)
            {
                destination.z = UnityEngine.Random.Range(-speed, 0.0f);

                time = 0.0f;
                wait = !wait;
            }
            if (transform.localPosition.z < boundary.zMin)
            {
                destination.z = UnityEngine.Random.Range(0.0f, speed);

                time = 0.0f;
                wait = !wait;
            }

            // if the move wait time has expired, sprite will need to stop and wait.
            if (time > moveWait)
            {
                destination.x = UnityEngine.Random.Range(-speed, speed);
                destination.z = UnityEngine.Random.Range(-speed, speed);

                time = 0.0f;
                wait = !wait;
            }

            transform.localPosition = new Vector3(transform.localPosition.x + destination.x,
                                                    0, transform.localPosition.z + destination.z);

            // ************************************* END OF COPIED WORK ****************************************      
    }


}