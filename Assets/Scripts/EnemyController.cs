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
    
	// Use this for initialization
	void Start () {
        rigidBod = this.GetComponent<Rigidbody>();
        StartCoroutine(moveEnemy());
        counter = 0.0;
    }

    //void Update ()
    //{
    //    float x, z;
    //    x = findNextLocation(transform.localPosition.x, boundary.xMin, boundary.xMax);
    //    z = findNextLocation(transform.localPosition.z, boundary.zMin, boundary.zMax);

    //    transform.localPosition = new Vector3(transform.localPosition.x + x, 0.0f, transform.localPosition.z + z );

    //}

    //float findNextLocation (float currentPosition, float min, float max)
    //{
    //    if (currentPosition > max)
    //    {
    //        return Random.Range(-speed, 0);
    //    }
    //    else if (currentPosition < min)
    //    {
    //        return Random.Range(0, speed);
    //    }

    //    return currentPosition;
    //}

    //void Update ()
    //{
    //    if (counter > moveWait)
    //    {

    //        // Deterimine random direction for enemy.
    //        Vector3 randVect = Random.insideUnitSphere * speed;

    //        // make sure it does not move outside of player's plane.
    //        randVect.y = 0.0f;

    //        // set velocity trajectory for enemy.
    //        rigidBod.velocity = randVect;

    //        this.rigidBod.position = new Vector3(Mathf.Clamp(rigidBod.position.x, boundary.xMin, boundary.xMax),
    //                                                 0.0f,
    //                                                 Mathf.Clamp(rigidBod.position.z, boundary.zMin, boundary.zMax));
    //        counter = 0.0;
    //    }

    //    counter += 0.1;

    //}

    /*
    *   Will enumerate through the enemies movement over time.
    */
    IEnumerator moveEnemy()
    {  
        // Deterimine random direction for enemy.
        Vector3 randVect = Random.insideUnitSphere * speed;
        // make sure it does not move outside of player's plane.
        randVect.y = 0.0f;

        Debug.Log(randVect);

        while (true)
        {
          

            while (rigidBod.position.x > boundary.xMin && rigidBod.position.x < boundary.xMax &&
                    rigidBod.position.z > boundary.zMin && rigidBod.position.z < boundary.zMax)
            {
                // set velocity trajectory for enemy.
                rigidBod.velocity = randVect;
               

                yield return new WaitForSeconds(moveWait);
            }

            // stop rigid body and wait to bounce
            rigidBod.velocity = Vector3.zero;
            yield return new WaitForSeconds(bounceWait);

            

            // Deterimine random direction for enemy.
            randVect = Random.insideUnitSphere * speed;
            // make sure it does not move outside of player's plane.
            randVect.y = 0.0f;

            rigidBod.velocity = randVect;

            yield return new WaitForSeconds(moveWait);

        }

    }
}
