using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    private GameController gameController;
    private int playerHealth;

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
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        playerHealth = 100;

        gameController.setHealth(playerHealth);
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
        }
        if (other.collider.tag.Equals("Enemy"))
        {
            playerHealth -= Random.Range(0, 10);
            if (playerHealth < 1)
            {
                gameController.GameOver();
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Accessing Health");
                gameController.setHealth(playerHealth);

            }
            
        }
    }
}
