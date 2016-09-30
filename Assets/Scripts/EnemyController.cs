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
	private new Transform transform;
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

    /*
    * This Enumerates for enemies movements. 
    */
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
			transform.DOMove (randVect , speed, false);
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

		leftProb = 1 - Mathf.Pow( (currPos / boundary.xMin), 2);
        rightProb = 1 - Mathf.Pow((currPos / boundary.xMax), 2);
	}

	/*
	 * Will calculate the probability range for the z range
	 */
	void calcZRange ()
	{
		float currPos = transform.position.z;
		botProb = 1 - Mathf.Pow( (currPos / boundary.zMin), 2);
        topProb = 1 - Mathf.Pow((currPos / boundary.zMax), 2);
	}
}
