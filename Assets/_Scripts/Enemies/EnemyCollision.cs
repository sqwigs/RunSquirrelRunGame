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
        {
            GetComponent<NavMeshAgent>().Stop();
        }
        else if (other.collider.tag.Equals("Arrow"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if(other.collider.tag.Equals("Spikes"))
        {
            Destroy(this.gameObject);
        }
    }

    /**
    *  If other enters the boundary, execute event
    */
    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            GetComponent<NavMeshAgent>().Resume();
        }
    }

}
