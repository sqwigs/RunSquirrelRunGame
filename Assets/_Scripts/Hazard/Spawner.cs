using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject hazard;
	public float spawnTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (spawnCycle ());
	}
	
	private IEnumerator spawnCycle () {
		while (true) {
			Vector3 spawnPosition = this.transform.position;
			Quaternion spawnRotation = this.transform.rotation;
			Instantiate (hazard, spawnPosition, spawnRotation);

			yield return new WaitForSeconds (spawnTime);

		}
	}
}
