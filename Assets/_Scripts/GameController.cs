using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // GUI Interface for user
    public Text healthText;
    public Text restartText;
    public Text gameOverText;
    public Text cooldownText;
    public GUIText levelCompleteText;

    // Controlling Var
    private bool gameOver;
	public int playerHealth;

    // Use this for initialization
    void Start ()
    {
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
                SceneManager.LoadSceneAsync(0);
            }
        }

		if (Input.GetKeyDown (KeyCode.T)) { // Developer Debugging Code
            //Application.LoadLevel (Application.loadedLevel);
            SceneManager.LoadSceneAsync("FirstLevelProto");
		}

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

		healthText.text = "Health : " + playerHealth;
        
	}

    /// <summary>
    /// If called, this function will end the level
    /// </summary>
    public void levelComplete()
    {
        flipTextSwitches();
        levelCompleteText.text = "LEVEL COMPLETE!";
        restartText.text = "Press R to Restart!";
        gameOver = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOver ()
	{
        flipTextSwitches();
        gameOverText.text = "Game Over!";
        playerHealth = 0;
        restartText.text = "Press R to Restart!";
		gameOver = true;
	}

	/// <summary>
	/// If the player is hit and dies, this function will return true, else returns false. 
	/// </summary>
	/// <returns><c>true</c>, if hit was playered, <c>false</c> otherwise.</returns>
	public bool playerHit () {
		playerHealth -= 10;

		if (playerHealth < 1) {
			GameOver ();
			return true;
		}

		return false;
	}

    /// <summary>
    /// Will fiip the restartText, cooldownText, and gameOverText active switchs
    /// </summary>
    private void flipTextSwitches ()
    {
        
        restartText.gameObject.SetActive(true);
        cooldownText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Will set the internal cooldown text
    /// </summary>
    /// <param name="isActive" >determines what the cooldown text will say to player. </param>
    public void setCooldownText (string status)
    {
        cooldownText.text = "Freeze : " + status;
    }
}
