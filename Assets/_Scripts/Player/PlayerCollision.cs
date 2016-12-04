using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    //private GUIController gameController;
    private PlayerController player;

    /// <summary>
    /// Run at the start of game
    /// </summary>
    void Start()
    {
        //GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GUIController");

        //if (gameControllerObject != null)
        //{
        //    gameController = gameControllerObject.GetComponent<GUIController>();
        //}
        //else
        //{
        //    Debug.Log("Cannot find 'GUIController' script");
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

    /**
   *  If player collides with an object, determine what other is and process accordingly
   *
   *   -Boudary : Stop Movement
   *   -Enemy : Destroy Player and intiate Game Over
   */
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Equals("Boundary"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }
        else if (other.collider.tag.Equals("Arrow"))
        {
            Destroy(other.gameObject);
        }

         player.playerHit(other.transform.position);
    }

}
