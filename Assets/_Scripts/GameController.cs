using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    // GUI Interface for user
    public GUIText healthText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText levelCompleteText;

    // Controlling Var
    private bool gameOver;
	public int playerHealth;

    // Use this for initialization
    void Start ()
    {
        restartText.text = "";
        gameOverText.text = "";
        levelCompleteText.text = "";
        gameOver = false;
    }

    // Update is called once per frame
    void Update ()
    {
	    if (gameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                //Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadSceneAsync("FirstLevelProto");
            }
        }
		if (Input.GetKeyDown (KeyCode.T)) {
            //Application.LoadLevel (Application.loadedLevel);
            SceneManager.LoadSceneAsync("FirstLevelProto");
		}

		healthText.text = "Health : " + playerHealth;
	}

    /// <summary>
    /// If called, this function will end the level
    /// </summary>
    public void levelComplete()
    {
        levelCompleteText.text = "LEVEL COMPLETE!";
        restartText.text = "Press R to Restart!";
        gameOver = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOver ()
	{
		gameOverText.text = "Game Over!";
        healthText.text = "Health : " + 0;
        restartText.text = "Press R to Restart!";
		gameOver = true;
	}

	/// <summary>
	/// If the player is hit and dies, this function will return true, else returns false. 
	/// </summary>
	/// <returns><c>true</c>, if hit was playered, <c>false</c> otherwise.</returns>
	public bool playerHit () {
		playerHealth -= UnityEngine.Random.Range (1, 10);

		if (playerHealth < 1) {
			GameOver ();
			return true;
		}

		return false;
	}
}
