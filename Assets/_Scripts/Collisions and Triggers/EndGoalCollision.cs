using UnityEngine;
using System.Collections;

public class EndGoalCollision : MonoBehaviour {

    private GUIController gameController;

    /// <summary>
    /// Run at the start of game
    /// </summary>
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GUIController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GUIController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GUIController' script");
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            gameController.GameOver = true;
            Destroy(other.gameObject);
        }
    }
}
