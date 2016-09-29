using UnityEngine;
using System.Collections;

public class BoundaryCollision : MonoBehaviour {

    /**
    *  If other enters the boundary, execute event
    */
	void OnTriggerEnter (Collider other)
    {
        // Stop Player
        if (other.tag.Equals("Player"))
        {

        }
        // Bounce Enemy
        else if (other.tag.Equals("Enemy"))
        {

        }
    }
}
