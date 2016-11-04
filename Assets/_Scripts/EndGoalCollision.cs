using UnityEngine;
using System.Collections;

public class EndGoalCollision : MonoBehaviour {

    private GameController gameController;

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
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            gameController.levelComplete();
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
