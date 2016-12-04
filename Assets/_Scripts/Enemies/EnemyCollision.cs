using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour
{
    public float timeStalled;

    /**
    *  If other enters the boundary, execute event
    */
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            StartCoroutine(recoil());
        }
    }

    private IEnumerator  recoil ()
    {
        GetComponent<NavMeshAgent>().Stop();

        yield return new WaitForSeconds(timeStalled);

        GetComponent<NavMeshAgent>().Resume();
    }

}
