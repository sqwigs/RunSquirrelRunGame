using UnityEngine;
using System.Collections;
using DG.Tweening;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    // Player Movement Control Variables
    public float speed;
    public Boundary boundary;

    // Used for movement control
    private Rigidbody rigidBod;
    private float moveHorizontal, moveVertical;
    private Vector3 currVector;

    void Start ()
    {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        rigidBod = this.GetComponent<Rigidbody>();
        currVector = rigidBod.position;

        if (speed <=0 )
        {
            speed = 10;
        }
    }
    
    void Update ()
    {
        // Determine vector to move character
        // Vector3 movementVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //rigidBod.velocity = movementVector * -speed;

        // get movementinput from user
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        currVector.x += moveHorizontal / -speed;
        currVector.z += moveVertical / -speed;

        rigidBod.DOMove(currVector, 0);
        
        rigidBod.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}
