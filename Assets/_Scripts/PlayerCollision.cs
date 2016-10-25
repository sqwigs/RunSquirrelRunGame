using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    private GameController gameController;
    private PlayerController player;
    public int playerHealth;

    /// <summary>
    /// Run at the start of game
    /// </summary>
    void Start()
    {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
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
            player.recoilReset();
        }
        if (other.collider.tag.Equals("Enemy"))
        {
            playerHealth -= Random.Range(1, 10);
            if (playerHealth < 1)
            {
                gameController.GameOver();
                Destroy(this.gameObject);
            }
            else
            {
                gameController.setHealth(playerHealth);
                player.recoil(other.transform.position);
                Debug.Log("Hitting Player");
            }
            
        }
    }
}
