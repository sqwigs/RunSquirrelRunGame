using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    // public movement var of Enemy
    public float speed;
	public Vector3 originPt;
    public float moveWait;
    public float bounceWait;
    public Boundary boundary;

    // Control for the Enemy
    private Rigidbody rigidBod;
    private Vector3 randVect;
    private float waitTime;

	// The probability of moving in a specific direction
	private double leftProb;
	private double rightProb;
	private double topProb;
	private double botProb;

	// Use this for initialization
	void Start () {
        rigidBod = this.GetComponent<Rigidbody>();
        // Deterimine Random direction for enemy.
        randVect = Random.insideUnitSphere;
        // make sure it does not move outside of player's plane.
        randVect.y = 0.0f;
        waitTime = moveWait;

        if (speed <= 0)
        {
            speed = 1;
        }

		leftProb = 50;
		rightProb = 50;
		topProb = 50;
		botProb = 50;
    }

    void Update ()
    {
        // change x velocity vector
        if (rigidBod.position.x <= boundary.xMin)
        {
            rigidBod.velocity = Vector3.zero;
            randVect.x = Random.Range(0, boundary.xMax);
            
        }
        else if (rigidBod.position.x >= boundary.xMax)
        {
            randVect.x = Random.Range(boundary.xMin, 0);
            rigidBod.velocity = Vector3.zero;
           
        }

        // change z velocity vector
        if (rigidBod.position.z <= boundary.zMin)
        {
            randVect.z = Random.Range(0, boundary.zMax);
            rigidBod.velocity = Vector3.zero;
           
        }
        else if (rigidBod.position.z >= boundary.zMax)
        {
            randVect.z = Random.Range(boundary.zMin, 0);
            rigidBod.velocity = Vector3.zero;
        }

        if(Time.time > waitTime)
        {
            waitTime = Time.time + moveWait;
			calcXRange();
            // make sure it does not move outside of player's plane.
            randVect.y = 0.0f;
            rigidBod.velocity = randVect.normalized * speed;
        }
        
        
    }


	void calcXRange () 
	{	
		float currPos = rigidBod.position.x;
		double probOfXMovement = Mathf.Pow( (currPos / originPt.x), 2);

		// If current x position is greater than 0, then the probability of moving left must be greater than right
		if (currPos > 0) 
		{
			leftProb = probOfXMovement;
			rightProb = 100 - probOfXMovement;
		} 
		// else right must be greater than the left
		else 
		{
			rightProb = probOfXMovement;
			leftProb = 100 - probOfXMovement;
		}

	}

	void calcZRange ()
	{
		float currPos = rigidBod.position.z;
		double probOfXMovement = Mathf.Pow( (currPos / originPt.z), 2);

		// If current z position is greater than 0, then the probability of moving down is greater than that of moving up
		if (currPos > 0) 
		{
			botProb = probOfXMovement;
			topProb = 100 - probOfXMovement;
		} 
		// else right must be greater than the left
		else 
		{
			topProb = probOfXMovement;
			botProb = 100 - probOfXMovement;
		}
	}
}
