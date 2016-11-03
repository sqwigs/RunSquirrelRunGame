using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{

    #region Member Variables

    // Player Movement Control Variables
    public float speed;

    // Used for movement control
    private Rigidbody rigidBod;
    private float moveHorz, moveVert;

    // Used for sprite control
    private SpriteRenderer[] spList;
    private int previousState;

    // Control if player was hit by enemy.
    public float recoilTime; // how much time the player will recoil backwards
    public float maxRecoilDist; // max distance that the player can recoil
    public float recoveryTime; // max amount of time before player regains control. 
    private bool collisionEnabled = true;

    // Freezing Power Control
    public float freezePowerCooldown;
    public float freezePowerActive;
    private bool freezeOn = true;
    private GameObject freezeSphere;
    private GameController gameController;

    #endregion

    void Start()
    {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        rigidBod = this.GetComponent<Rigidbody>();

        // Initializes and sets the sprite object to switch to when direction changes
        spList = gameObject.GetComponentsInChildren<SpriteRenderer>();

        previousState = 2; // intial state facing the right

        if (speed <= 0)
        {
            speed = 10;
        }

        // get the detection sphere game object to handle
        if (!GetChild(this.gameObject, "DetectionSphere", out freezeSphere))
        {
            Debug.Log("Could not find child \"DetectionSphere\" of player object");
        }

        // retrieve GameController Object
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        // turn on all freezing controls
        freezeSphere.SetActive(false);
        gameController.setCooldownText("ON");
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
        if (collisionEnabled)
		{
            // Determine vector to move character
            Vector3 movementVector = new Vector3(moveHorz, 0.0f, moveVert);
            rigidBod.velocity = movementVector * -speed;
        }

    }

    void FixedUpdate()
    {
        // get movementinput from user
        moveHorz = Input.GetAxis("Horizontal");
        moveVert = Input.GetAxis("Vertical");

        changeHorzSpriteDirection(moveHorz);
        setVertSpriteDirection(moveVert);

        if (Input.GetKeyDown(KeyCode.F) && freezeOn)
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

        gameController.setCooldownText("ACTIVE");

        yield return new WaitForSeconds(freezePowerActive);

        freezeSphere.SetActive(false); // turn off detection sphere

        gameController.setCooldownText("OFF");

        yield return new WaitForSeconds(freezePowerCooldown);

        freezeOn = true;

        gameController.setCooldownText("ON"); // turn on detection sphere

    }
    #endregion

    #region player collision control

    /// <summary>
    /// If player is hit by enemy, then player must move in the opposite direction of the enemies vector.
    /// </summary>
    private IEnumerator recoil (Vector3 force)
	{
		rigidBod.AddForce(force * maxRecoilDist);

		yield return new WaitForSeconds (recoveryTime);

		collisionEnabled = true;

	}

    /// <summary>
    /// When this method is called, the recoil subroutine will be started. 
    /// </summary>
    /// <param name="enemyContact"></param>
	public void playerHit (Vector3 enemyContact) {
        if (collisionEnabled)
        {
            collisionEnabled = false;
            StartCoroutine(recoil(enemyContact));
        }
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

    #region Sprite Direction

    /// <summary>
    /// Sets the sprite's vertical direction based on param vertDir
    /// </summary>
    /// <param name="vertDir"></param>
    private void setVertSpriteDirection(float vertDir)
    {
        // Change Sprite if necessary
        if (vertDir < 0 && previousState != 0)
        {
            spList[0].enabled = true;
            spList[previousState].enabled = false;
            previousState = 0;
        }
        else if (vertDir > 0 && previousState != 1)
        {
            spList[1].enabled = true;
            spList[previousState].enabled = false;
            previousState = 1;
        }
    }

    /// <summary>
    /// Sets the sprite's horizontal direction based on param horzDir
    /// </summary>
    /// <param name="horzDir"></param>
    private void changeHorzSpriteDirection(float horzDir)
    {
        // Change Sprite if necessary
        if (horzDir < 0 && previousState != 3)
        {
            spList[3].enabled = true;
            spList[previousState].enabled = false;
            previousState = 3;
        }
        else if (horzDir > 0 && previousState != 2)
        {
            spList[2].enabled = true;
            spList[previousState].enabled = false;
            previousState = 2;
        }
    }
    #endregion
}
