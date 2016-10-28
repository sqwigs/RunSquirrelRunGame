using UnityEngine;
using System.Collections;

public class DetectionTrigger : MonoBehaviour {

    public GameObject EnemyController;
    private DogNavigation _nav;

    void Start ()
    {
        _nav = EnemyController.GetComponent<DogNavigation>();
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
