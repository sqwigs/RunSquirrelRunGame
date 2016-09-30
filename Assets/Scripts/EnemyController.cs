using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyController : MonoBehaviour {

    // public movement var of Enemy
    public float speed;
    public float moveWait;
    public float bounceWait;
	public float pauseWait;
	public Vector3 originPt;
    public Boundary boundary;

    // Control for the Enemy
	private Transform transform;
    private Vector3 randVect;

	// The probability of moving in a specific direction
	private float leftProb;
	private float rightProb;
	private float topProb;
	private float botProb;

	// Use this for initialization
	void Start () {
		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

		transform = this.GetComponent<Transform>();
        // Deterimine Random direction for enemy.
		randVect = new Vector3();
        // make sure it does not move outside of player's plane.
        randVect.y = 0.0f;

        if (speed <= 0)
        {
            speed = 1;
        }

		leftProb = 50;
		rightProb = 50;
		topProb = 50;
		botProb = 50;

		StartCoroutine (moveEnemy());
    }

	// Runs every frame
//    void Update ()
//    {
//        if(Time.time > waitTime)
//        {
//            waitTime = Time.time + moveWait;
//			calcXRange ();
//			calcZRange ();
//			randVect.x = Random.Range (boundary.xMin * leftProb, boundary.xMax * rightProb);
//			randVect.z = Random.Range (boundary.zMin * botProb, boundary.zMax * topProb);
//			Debug.Log (randVect.x);
//			Debug.Log (randVect.z);
//            // make sure it does not move outside of player's plane.
//            randVect.y = 0.0f;
//			transform.DOMove (randVect, speed, false);
//            //rigidBod.velocity = randVect.normalized * speed;
//        }
//    }
//
	IEnumerator moveEnemy () 
	{
		while (true) 
		{
			calcXRange ();
			calcZRange ();
			randVect.x = Random.Range (boundary.xMin * leftProb, boundary.xMax * rightProb);
			randVect.z = Random.Range (boundary.zMin * botProb, boundary.zMax * topProb);
			// make sure it does not move outside of player's plane.
			randVect.y = 0.0f;
			transform.DOMove (randVect, speed, false);
			//rigidBod.velocity = randVect.normalized * speed;

			yield return new WaitForSeconds (moveWait);

			transform.DOPause ();

			yield return new WaitForSeconds (pauseWait);
		}
	}

	/*
	 * Will calculate the probability range for the x range
	 */
	void calcXRange () 
	{	
		float currPos = transform.position.x;

		float probOfXMovement = Mathf.Pow( (currPos / originPt.x), 2);


		if (probOfXMovement == 0) 
		{
			leftProb = 0.5f;
			rightProb = 0.5f;
		}
		// If current x position is greater than 0, then the probability of moving left must be greater than right
		else if (currPos > 0) 
		{
			leftProb = probOfXMovement;
			rightProb = 1 - probOfXMovement;
		} 
		// else right must be greater than the left
		else 
		{
			rightProb = probOfXMovement;
			leftProb = 1 - probOfXMovement;
		}

	}

	/*
	 * Will calculate the probability range for the z range
	 */
	void calcZRange ()
	{
		float currPos = transform.position.z;
		float probOfXMovement = Mathf.Pow( (currPos / originPt.z), 2);

		if (probOfXMovement == 0) 
		{
			botProb = 0.5f;
			topProb = 0.5f;
		}
		// If current z position is greater than 0, then the probability of moving down is greater than that of moving up
		else if (currPos > 0) 
		{
			botProb = probOfXMovement;
			topProb = 1 - probOfXMovement;
		} 
		// else right must be greater than the left
		else 
		{
			topProb = probOfXMovement;
			botProb = 1 - probOfXMovement;
		}
	}
}
