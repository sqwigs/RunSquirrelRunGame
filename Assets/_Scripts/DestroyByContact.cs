using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
		
	void OnCollisionEnter (Collision other) {
		if (other.collider.tag.Equals("Boundary") )
		{
			Debug.Log ("Why!?!?!");
			Destroy (this.gameObject);
		}
	}
}
