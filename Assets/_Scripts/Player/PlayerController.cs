using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{

    #region Member Variables

	private bool inOSX;

    // Player Movement Control Variables
    public float speed;

    // Used for movement control
    private Rigidbody rigidBod;
    private float moveHorz, moveVert;

    // Used for model control
	private Quaternion rotation;
    private Animator animu;

    // Control if player was hit by enemy.
    public float maxRecoilDist; // max distance that the player can recoil
    public float recoveryTime; // max amount of time before player regains control. 
    public float respawnTime;
    public float invFrames;
    private bool collisionEnabled = true;

    // starting position of player
    private Vector3 startingPos;

    // Player Health
    public int playerHealth;
    private bool playerDead;
    private int playerFullHealth;

    // Freezing Power Control
    public float freezePowerCooldown;
    public float freezePowerActive;
    private bool freezeOn = true;
    private GameObject freezeSphere;
    private GUIController gameController;

    #endregion

    void Start()
    {

		if (Application.platform == RuntimePlatform.OSXEditor) {
			inOSX = true;
		} else {
			inOSX = false;
		}

        DOTween.Init(true, false, LogBehaviour.ErrorsOnly); // DOTween Intialziation.

        rigidBod = this.GetComponent<Rigidbody>(); 

		rotation = transform.rotation;

		GameObject squirrelMeshObj;

        if (speed <= 0)
        {
            speed = 10;
        }
        
        #region childrenCollection

        // get the detection sphere game object to handle
        if (!GetChild(this.gameObject, "DetectionSphere", out freezeSphere))
        {
            Debug.Log("Could not find child \"DetectionSphere\" of player object");
        }

		//// get the animator game object to handle
		//if (!GetChild(this.gameObject, "Running", out runningAnimu))
		//{
		//	Debug.Log("Could not find child \"Running\" of player object");
		//}

  //      // get the animator game object to handle
  //      if (!GetChild(this.gameObject, "Idle", out idleAnimu))
  //      {
  //          Debug.Log("Could not find child \"Idle\" of player object");
  //      }

        //// retrieve the meshes for the squirrel recoil effect. 
        //if (!GetChild(this.gameObject, "squirrelMesh", out squirrelMeshObj, true))
        //{
        //    Debug.Log("Could not find child \"Squirrel Mesh\" of player object");
        //}
        //else
        //{
        //    squirrelMesh = squirrelMeshObj.GetComponent<SkinnedMeshRenderer>();
        //}



        #endregion

        // retrieve GUIController Object
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GUIController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GUIController>();
        }
        else
        {
            Debug.Log("Cannot find 'GUIController' script");
        }

        animu = GetComponent<Animator>();

        // turn on all freezing controls
        freezeSphere.SetActive(false);

        startingPos = this.transform.position;
        playerFullHealth = playerHealth;
        playerDead = false;
    }

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy () {
		DOTween.Clear ();
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
    private bool GetChild(GameObject inside, string wanted, out GameObject childObject, bool recursive = false)
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


    #region Updates

    void Update()
    {
        // recoil of player if hit
        if (!playerDead && collisionEnabled && (Math.Abs(moveHorz) > 0.3 || Math.Abs(moveVert) > 0.3))
        {
            // Determine vector to move character
            Vector3 movementVector = new Vector3(moveHorz, 0.0f, moveVert);
            float tempSpeed = speed;

            // Animation run 
            animu.SetBool("Running", true);

            if (Math.Abs(moveHorz) > 0.9 || Math.Abs(moveVert) > 0.9)
            {
                tempSpeed *= 1.2f;
            }
            else {
                tempSpeed *= 0.5f;
            }
            rotation = Quaternion.LookRotation(-1 * movementVector);

            rigidBod.velocity = movementVector * -(tempSpeed);
            transform.rotation = rotation;
        }
        else {
            animu.SetBool("Running", false);
            rigidBod.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
		

        if (Input.GetMouseButtonDown(0))
        {
            moveHorz = Input.GetAxis("Mouse X");
            moveVert = Input.GetAxis("Mouse Y");
        }
        else
        {
            // get movementinput from user
            moveHorz = Input.GetAxis("Horizontal");
            moveVert = Input.GetAxis("Vertical");
       }


		if (inOSX) {
			if (Input.GetKeyDown(KeyCode.JoystickButton6)) {
				freezeOn = false;
				StartCoroutine(freeze());
			}
			if (Input.GetKeyDown(KeyCode.JoystickButton7)) {
				freezeOn = false;
				StartCoroutine(freeze());
			}
		}

		if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button2)
                                            || Input.GetKeyDown(KeyCode.Joystick1Button18)) 
                                                && freezeOn)
		{
			freezeOn = false;
			StartCoroutine(freeze());
		}
    }



    #endregion

    #region freeze ability
    /// <summary>
    /// Freezing Coroutine that will turn freezing ability on, thus freezing all enemies in range. Then after freezePowerActive seconds
    /// have passed, the ability will shut down and will go into cooldown until freezePowerCooldown time has expired. 
    /// </summary>
    private IEnumerator freeze()
    {
        freezeSphere.SetActive(true); // turn on detection sphere

        yield return new WaitForSeconds(freezePowerActive);

        freezeSphere.SetActive(false); // turn off detection sphere
        gameController.freezeActive(freezeOn = false);

        yield return new WaitForSeconds(freezePowerCooldown);

        gameController.freezeActive(freezeOn = true);

    }
    #endregion

    #region player collision control

    /// <summary>
    /// If player is hit by enemy, then player must move in the opposite direction of the enemies vector.
    /// </summary>
    private IEnumerator recoil (Vector3 force)
	{
        this.gameObject.GetComponent<Collider>().enabled = false;


        // TODO: Add recoil for down and up directional vectors
        if (Vector3.Angle(Vector3.right, rigidBod.velocity) > 90)
        {
            transform.DOMove(transform.position - (transform.position.normalized * maxRecoilDist), recoveryTime);
        }
        else
        {
            transform.DOMove(transform.position + (transform.position.normalized*maxRecoilDist), recoveryTime);
        }

        //squirrelMesh.material = hitMat;

        yield return new WaitForSeconds (recoveryTime);

        animu.SetBool("Hit", false);


        transform.DOKill();

        //squirrelMesh.material = baseMat;
        this.gameObject.GetComponent<Collider>().enabled = true;
        collisionEnabled = true;

	}

    /// <summary>
    /// When this method is called, the recoil subroutine will be started. 
    /// </summary>
    /// <param name="enemyContact"></param>
	public void playerHit ( Vector3 enemyContact) {
        if (collisionEnabled && !playerDead)
        {

            if ((playerHealth -= 10) < 1)
            {
                playerDead = true;
                gameController.TimerActive = false;
                animu.SetTrigger("Dead");
                StartCoroutine(playerRespawn());

            }
            else
            {
                animu.SetBool("Hit", true);

                collisionEnabled = false;
                StartCoroutine(recoil(enemyContact));
            }

            gameController.cyclePlayerHealth();
        }
	}

    public void debugPlayerHit()
    {
        playerHit(-this.transform.position);
    }

    /// <summary>
    /// Returns whether the player has been hit recently, thus in invinsible frames. 
    /// </summary>
    /// <returns> value of isHit private variable </returns>
    public bool getCollisionEnabled ()
    {
        return collisionEnabled;
    }

    #endregion

    /// <summary>
    /// Will stop player control until after death animation finishes, and then will respawn player at beginning of maze. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator playerRespawn()
    {
        yield return new WaitForSeconds(respawnTime); // Waits until animation state is Idle

        animu.SetTrigger("Alive");

        this.transform.position = startingPos;

        playerHealth = playerFullHealth;

        animu.ResetTrigger("Dead");

        playerDead = false;

        gameController.TimerActive = true;

        gameController.resetGUI();
    }
}
