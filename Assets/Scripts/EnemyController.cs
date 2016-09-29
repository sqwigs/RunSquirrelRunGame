using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    // Speed of Enemy
    public float speed;
    public float moveWait;
    public float bounceWait;
    public Boundary boundary;

    // Control for the Enemy
    private Rigidbody rigidBod;
    private double counter;
    private Vector3 randVect;
    private float waitTime;
    
	// Use this for initialization
	void Start () {
        rigidBod = this.GetComponent<Rigidbody>();
        counter = 0.0;
        // Deterimine Random direction for enemy.
        randVect = Random.insideUnitSphere;
        // make sure it does not move outside of player's plane.
        randVect.y = 0.0f;
        waitTime = moveWait;

        if (speed <= 0)
        {
            speed = 1;
        }
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
            randVect = Random.insideUnitSphere;
            // make sure it does not move outside of player's plane.
            randVect.y = 0.0f;
            rigidBod.velocity = randVect.normalized * speed;
        }
        
        
    }

}
