using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    private Transform player;
	private float zOffset;

	// Use this for initialization
	void Start ()
    {
        GameObject playerGameObject = GameObject.Find("Player");
		if (playerGameObject != null) {
			player = playerGameObject.transform;
			zOffset = transform.position.z - player.position.z;
		} else {
			Debug.LogError ("Could not find \"Player\" game object");
		}

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player != null)
        {	
            Vector3 playerpos = player.position;

            playerpos.y = transform.position.y;
            playerpos.z += zOffset;
            transform.position = playerpos;
        }
       
	}
}
