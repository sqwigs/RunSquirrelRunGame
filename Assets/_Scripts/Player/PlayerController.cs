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
    //private SpriteRenderer spriteRend;
    private float moveHorz, moveVert;

    // Used for sprite control
    private SpriteRenderer[] spList;
    private int previousState;

    // Control if player was hit by enemy.
    public float recoilTime; // how much time the player will recoil backwards
    public float maxRecoilDist; // max distance that the player can recoil
    public float recoveryTime; // max amount of time before player regains control. 
    private bool isHit;


    //private float currTime; // control for time. 
    private Vector3 playerSnapDir; // snapshot of players current direction vector.

    #endregion

    void Start()
    {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        rigidBod = this.GetComponent<Rigidbody>();
        //spriteRend = this.GetComponent<SpriteRenderer>();

        // Initializes and sets the sprite object to switch to when direction changes
        spList = gameObject.GetComponentsInChildren<SpriteRenderer>();

        previousState = 2; // intial state facing the right

        if (speed <= 0)
        {
            speed = 10;
        }

        //currTime = Time.deltaTime;
    }

    #region Updates

    void Update()
    {
        // recoil of player if hit
        if (!isHit)
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

		isHit = false;

	}

    /// <summary>
    /// When this method is called, the recoil subroutine will be started. 
    /// </summary>
    /// <param name="enemyContact"></param>
	public void playerHit (Vector3 enemyContact) {
        if (!isHit)
        {
            isHit = true;
            StartCoroutine(recoil(enemyContact));
        }
	}

    /// <summary>
    /// Returns whether the player has been hit recently, thus in invinsible frames. 
    /// </summary>
    /// <returns> value of isHit private variable </returns>
    public bool getIsHit ()
    {
        return isHit;
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
