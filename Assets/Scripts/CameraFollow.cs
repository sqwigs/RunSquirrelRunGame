using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 playerpos = player.transform.position;
        playerpos.y = 13;
        transform.position = playerpos;
	}
}
