using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(3);
        }
    }
}
