using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour
{
    private Rigidbody rigidbod;
    // Runs once at beginning
    void Start()
    {
        rigidbod = GetComponent<Rigidbody>();
    }

    /**
    *  If other enters the boundary, execute event
    */
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Equals("Player"))
            rigidbod.velocity = Vector3.zero;
    }

}
