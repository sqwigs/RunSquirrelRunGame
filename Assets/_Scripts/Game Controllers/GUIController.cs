using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    private HealthControl healthUI;
    public GameObject freezeFlagGUI;
    private FlagState freezeFlag;

    // Controlling Var
    private bool gameOver;
    public bool GameOver 
        {
            get { return gameOver;  }

            set { gameOver = value; }
        }

    // Pause Menu Control
    private GameObject pauseMenu;
    private bool paused;

    // time control
    public float totalTime;
    public GameObject timerPanel;
    private Text timerText;
    private Timer timer;

    // Use this for initialization
    void Start ()
    {
       // levelCompleteText.text = "";
        gameOver = false;
        paused = false;

        GameObject healthUIObject = GameObject.FindGameObjectWithTag("HealthUI");

        // Locates the healthUIObject
        if (healthUIObject != null)
        {
            healthUI = healthUIObject.GetComponent<HealthControl>();
        }
        else
        {
            Debug.Log("Cannot find 'HealthUI' script");
        }

       

        // Gets pause menu gameobject
        if (!GetChild(this.gameObject, "PauseMenu", out pauseMenu))
        {
            Debug.Log("Could not find \"PauseMenu\" for " + this.name + " game object");
        }

        // Get TimerText
        timerText = timerPanel.GetComponent<Text>();
        if(timerText == null)
        {
            GetChild(timerPanel, "TimerText", out timerPanel, true);
            timerText = timerPanel.GetComponent<Text>();
        }
        timer = new Timer(totalTime);

        freezeFlag = freezeFlagGUI.GetComponent<FlagState>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!paused)
        {
            if (Input.GetKeyDown(KeyCode.T))
            { // Developer Debugging Code
                SceneManager.LoadSceneAsync("FirstLevelProto");
            }

            pauseControl(true);

            timerText.text = "TIME TO FIND ACORN\n" + timer.ToString();
            timer.UpdateTimer();

            if (timer.TimeLeft < 1)
            {
                paused = false;
                MainMenu();
            }
        }
        else
        {
            pauseControl(false);


        }
        
	}

    /// <summary>
    /// Determine if key was pressed to intiate pause control and will set the state of pause menu to give value if key was pressed. 
    /// </summary>
    /// <param name="state"></param>
    private void pauseControl (bool state)
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            pauseMenu.SetActive(state);
            paused = state;
            Time.timeScale = (state) ? 0 : 1;

        }
    }

	/// <summary>
	/// If the player is hit and dies, this function will return true, else returns false. 
	/// </summary>
	/// <returns><c>true</c>, if hit was playered, <c>false</c> otherwise.</returns>
	public void cyclePlayerHealth () {

        healthUI.healthDecrease();
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

    public void MainMenu ()
    {
        SceneManager.LoadScene(0); // should be main menu
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

    /// <summary>
    /// Based on the bool given,
    /// will turn the flag gui item representing the freeze ability on or off. 
    /// </summary>
    /// <param name="active"> status of freeze ability </param>
    internal void freezeActive(bool active)
    {
        if (active)
        {
            freezeFlag.flagActive();
        }
        else
        {
            freezeFlag.flagCooldown();
        }
    }
}
