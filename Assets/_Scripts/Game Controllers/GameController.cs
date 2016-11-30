using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // GUI Interface for user
    //public Text healthText;
    //public Text restartText;
    //public Text gameOverText;
    //public Text cooldownText;
    //public GUIText levelCompleteText;

    private HealthControl healthUI;

    // Controlling Var
    private bool gameOver;
	public int playerHealth;

    // Use this for initialization
    void Start ()
    {
       // levelCompleteText.text = "";
        gameOver = false;

        GameObject healthUIObject = GameObject.FindGameObjectWithTag("HealthUI");

        if (healthUIObject != null)
        {
            healthUI = healthUIObject.GetComponent<HealthControl>();
        }
        else
        {
            Debug.Log("Cannot find 'HealthUI' script");
        }
    }

    // Update is called once per frame
    void Update ()
    {
	    if (gameOver)
        {
            SceneManager.LoadScene(0); // should be main menu
        }

		if (Input.GetKeyDown (KeyCode.T)) { // Developer Debugging Code
            SceneManager.LoadSceneAsync("FirstLevelProto");
		}

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

		//healthText.text = "Health : " + playerHealth;
        
	}

    /// <summary>
    /// If called, this function will end the level
    /// </summary>
    public void levelComplete()
    {
        gameOver = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOver ()
	{
        playerHealth = 0;
		gameOver = true;
	}

	/// <summary>
	/// If the player is hit and dies, this function will return true, else returns false. 
	/// </summary>
	/// <returns><c>true</c>, if hit was playered, <c>false</c> otherwise.</returns>
	public bool playerHit () {
		playerHealth -= 10;
        healthUI.healthUpdate();

		if (playerHealth < 1) {
			GameOver ();
			return true;
		}

		return false;
	}

    /// <summary>
    /// Will set the internal cooldown text
    /// </summary>
    /// <param name="isActive" >determines what the cooldown text will say to player. </param>
    public void setCooldownText (string status)
    {
        //cooldownText.text = "Freeze : " + status;
    }
}
