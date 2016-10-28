using UnityEngine;
using System.Collections;

public class HazardTrigger : MonoBehaviour {

	private GameController gameController;
	private PlayerController player;


	/// <summary>
	/// Run at the start of game
	/// </summary>
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");

		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		else
		{
			Debug.Log("Cannot find 'GameController' script");
		}

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

	void OnTriggerEnter (Collider collider) 
	{
		if (collider.tag.Equals("Player") )
		{
			//player.recoil (-collider.transform.position);
			if (gameController.playerHit ()) 
			{
				gameController.GameOver ();
				Destroy (collider.gameObject);
			}
		}
	}
}
