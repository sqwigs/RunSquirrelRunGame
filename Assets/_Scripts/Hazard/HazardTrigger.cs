using UnityEngine;
using System.Collections;

public class HazardTrigger : MonoBehaviour {

	private PlayerController player;


	/// <summary>
	/// Run at the start of game
	/// </summary>
	void Start()
	{
		//GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GUIController");

		//if (gameControllerObject != null)
		//{
		//	gameController = gameControllerObject.GetComponent<GUIController>();
		//}
		//else
		//{
		//	Debug.Log("Cannot find 'GUIController' script");
		//}

		GameObject playerControllerObject = GameObject.FindGameObjectWithTag("Player");

		if (playerControllerObject != null)
		{
			player = playerControllerObject.GetComponent<PlayerController>();
		}
		else
		{
			Debug.Log("Cannot find 'PlayerController' script");
		}
			
	}

	void OnCollisionEnter (Collision collision) 
	{
		if (collision.gameObject.tag.Equals("Player"))
		{
            player.playerHit(player.transform.position);
		}
	}
}
