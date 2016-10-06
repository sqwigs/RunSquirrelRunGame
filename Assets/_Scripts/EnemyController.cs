using UnityEngine;
using System.Collections;
using DG.Tweening;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class EnemyController : MonoBehaviour
{

    // public movement var of Enemy
    public float speed;
    public float moveWait;
    public float pauseWait;
    //public Vector3 originPt;
    public Boundary boundary;

    // Control for the Enemy
    private new Transform transform;
    private Vector3 randDest;
    private SpriteRenderer spriteRend;

    // The probability of moving in a specific direction
    //private float leftProb;
    //private float rightProb;
    //private float topProb;
    //private float botProb;

    private float time;
    private bool wait;

    //	// Use this for initialization
    //	void Start () {
    //		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

    //        // get Enemy game object components
    //		transform = this.GetComponent<Transform>();
    //        spriteRend = this.GetComponent<SpriteRenderer>();

    //        // Deterimine Random direction for enemy.
    //		randDest = new Vector3();

    //        // make sure it does not move outside of player's plane.
    //        randDest.y = 0.0f;

    //        if (speed <= 0)
    //        {
    //            speed = 1;
    //        }

    //		leftProb = 1;
    //		rightProb = 1;
    //		topProb = 1;
    //		botProb = 1;

    //		StartCoroutine (moveEnemy());
    //    }

    //    /*
    //    * This Enumerates for enemies movements. 
    //    */
    //	IEnumerator moveEnemy () 
    //	{
    //		while (true) 
    //		{
    //            Vector3 origin = randDest; // copy or ref?
    //			calcXRange ();
    //			calcZRange ();
    //			randDest.x = Random.Range (boundary.xMin * leftProb, boundary.xMax * rightProb);
    //			randDest.z = Random.Range (boundary.zMin * botProb, boundary.zMax * topProb);
    //			// make sure it does not move outside of player's plane.
    //			randDest.y = 0.0f;

    //            if ((randDest.normalized.x - origin.normalized.x) < 0)
    //            {
    //                spriteRend.flipX = true;
    //            }
    //            else if ((randDest.normalized.x - origin.normalized.x) > 0)
    //            {
    //                spriteRend.flipX = false;
    //            }

    //			transform.DOMove (randDest , speed*randDest.magnitude, false);

    //            //rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    //            yield return new WaitForSeconds (Random.Range(1, moveWait));

    //			transform.DOPause ();

    //			yield return new WaitForSeconds (Random.Range(1, pauseWait));
    //		}
    //	}

    //	/*
    //	 * Will calculate the probability range for the x range
    //	 */
    //	void calcXRange () 
    //	{	
    //		float currPos = transform.position.x;

    //		leftProb = Mathf.Pow( (currPos / boundary.xMin), 2);
    //        rightProb = 1 - rightProb;
    //	}

    //	/*
    //	 * Will calculate the probability range for the z range
    //	 */
    //	void calcZRange ()
    //	{
    //		float currPos = transform.position.z;
    //        topProb = Mathf.Pow( (currPos / boundary.zMax), 2);
    //        botProb = 1 - botProb;
    //    }
    //}


    void Start()
    {
        transform = GetComponent<Transform>();
        randDest = new Vector3(0, 0, 0);
        speed /= 100;
    }

   void Update ()
    {
        if (!wait)
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
                randDest.x = Random.Range(-speed, 0.0f);

                time = 0.0f;
                wait = !wait;
            }
            if (transform.localPosition.x < boundary.xMin)
            {
                randDest.x = Random.Range(0.0f, speed);

                time = 0.0f;
                wait = !wait;
            }
            if (transform.localPosition.z > boundary.zMax)
            {
                randDest.z = Random.Range(-speed, 0.0f);

                time = 0.0f;
                wait = !wait;
            }
            if (transform.localPosition.z < boundary.zMin)
            {
                randDest.z = Random.Range(0.0f, speed);

                time = 0.0f;
                wait = !wait;
            }

            // if the move wait time has expired, sprite will need to stop and wait.
            if (time > moveWait)
            {
                randDest.x = Random.Range(-speed, speed);
                randDest.z = Random.Range(-speed, speed);

                time = 0.0f;
                wait = !wait;
            }

            transform.localPosition = new Vector3(transform.localPosition.x + randDest.x,
                                                    0, transform.localPosition.z + randDest.z);

// ************************************* END OF COPIED WORK ****************************************
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

}