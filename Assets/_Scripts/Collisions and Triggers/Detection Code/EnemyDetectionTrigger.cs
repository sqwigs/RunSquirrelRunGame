using UnityEngine;
using System.Collections;

public class EnemyDetectionTrigger : MonoBehaviour {

    private Navigable _nav;

    void Start ()
    {
        _nav = GetComponentInParent<Navigable>();
    }
	
	void OnTriggerEnter (Collider other) {
        if (other.tag.Equals("Player")) {
            _nav.TargetFound(other.transform.position);
        }
    }

    void OnTriggerStay (Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _nav.TargetFound(other.transform.position);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _nav.TargetLost(other.transform.position);
        }
    }
}
