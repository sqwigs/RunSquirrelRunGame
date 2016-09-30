using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BoundaryCollision : MonoBehaviour {

	private EnemyController eC;


	void Start () 
	{
		eC = GameObject.Find ("Enemy").GetComponent<EnemyController> ();
	}

    /**
    *  If other enters the boundary, execute event
    */
	void OnTriggerEnter (Collider other)
    {
		Debug.Log (other.tag);
        // Stop Player
        if (other.tag.Equals("Player"))
        {

        }
        // Bounce Enemy
        else if (other.tag.Equals("Enemy"))
        {
			Debug.Log ("Trying to Pause");
			eC.DOKill();
        }
    }
}
