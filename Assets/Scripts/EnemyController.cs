using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    // Speed of Enemy
    public float speed;

    // Control for the Enemy
    private Rigidbody rigidBod;
    public float moveWait;
    private float waitTime;

	// Use this for initialization
	void Start () {
        rigidBod = this.GetComponent<Rigidbody>();
        waitTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > moveWait)
        {
            waitTime = Time.time + moveWait;
            Vector3 randVect = Random.insideUnitSphere * speed;

            randVect.y = 0.0f;

            // rigidBod.velocity = randVect * speed;

            this.transform.Translate(randVect);
        }
        
    }
}
