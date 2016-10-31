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

	void OnCollisionEnter (Collision collision) 
	{
		if (collision.gameObject.tag.Equals("Player"))
		{
			//player.recoil (-collider.transform.position);
			if (gameController.playerHit ()) 
			{
				gameController.GameOver ();
				Destroy (collision.gameObject);
			}
		}
	}
}
