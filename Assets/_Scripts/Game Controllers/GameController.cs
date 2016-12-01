using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private HealthControl healthUI;

    // Controlling Var
    private bool gameOver;
	public int playerHealth;

    // Pause Menu Control
    private GameObject pauseMenu;
    private bool paused;

    // Use this for initialization
    void Start ()
    {
       // levelCompleteText.text = "";
        gameOver = false;
        paused = false;

        GameObject healthUIObject = GameObject.FindGameObjectWithTag("HealthUI");

        if (healthUIObject != null)
        {
            healthUI = healthUIObject.GetComponent<HealthControl>();
        }
        else
        {
            Debug.Log("Cannot find 'HealthUI' script");
        }

        if (!GetChild(this.gameObject, "PauseMenu", out pauseMenu))
        {
            Debug.Log("Could not find \"PauseMenu\" for " + this.name + " game object");
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (!paused)
        {
            if (gameOver)
            {
                SceneManager.LoadScene(0); // should be main menu
            }

            if (Input.GetKeyDown(KeyCode.T))
            { // Developer Debugging Code
                SceneManager.LoadSceneAsync("FirstLevelProto");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(true);
                paused = true;
                Time.timeScale = 0;
            }
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

    /// <summary>
    /// When this method is called, it will unpause the game.
    /// </summary>
    public void unpause()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Gets the child gameObject whose name is specified by 'wanted'
    /// The search is non-recursive by default unless true is passed to 'recursive'
    /// 
    /// Will return bool if child was found and place that child in childObject out param.
    /// 
    /// ********************* USED FROM THE FOLLOWING SOURCE *************************
    /// http://answers.unity3d.com/questions/726780/disabling-child-gameobject-from-script-attached-to.html
    /// 
    /// ******************************************************************************
    /// 
    /// </summary>
    protected bool GetChild(GameObject inside, string wanted, out GameObject childObject, bool recursive = false)
    {
        childObject = null;
        foreach (Transform child in inside.transform)
        {
            if (child.name == wanted)
            {
                childObject = child.gameObject;
                return true;
            }
            if (recursive)
            {
                var within = GetChild(child.gameObject, wanted, out childObject, true);
                if (within) return within;
            }
        }
        return false;
    }
}
