using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{
    // Player Movement Control Variables
    public float speed;

    // Used for movement control
    private Rigidbody rigidBod;
    //private SpriteRenderer spriteRend;
    private float moveHorizontal, moveVertical;

    // Used for sprite control
    private SpriteRenderer[]  spList;
    private int previousState;

    void Start ()
    {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        rigidBod = this.GetComponent<Rigidbody>();
        //spriteRend = this.GetComponent<SpriteRenderer>();

        // Initializes and sets the sprite object to switch to when direction changes
        spList = gameObject.GetComponentsInChildren<SpriteRenderer>();

        previousState = 2; // intial state facing the right

        if (speed <=0 )
        {
            speed = 10;
        }
    }
    
    void Update ()
    {
        // Determine vector to move character
        Vector3 movementVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBod.velocity = movementVector * -speed;

       // rigidBod.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        // get movementinput from user
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        changeHorzSpriteDirection(moveHorizontal);
        changeVertSpriteDirection(moveVertical);
    }

    private void changeVertSpriteDirection(float vertDir)
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

    private void changeHorzSpriteDirection (float horzDir)
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
}
