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
    public float speed = 10;
    public Boundary boundary;

    // Used for movement control
    private CharacterController charControl;
    private Rigidbody rigidBod;
    private float moveHorizontal, moveVertical;


    void Start ()
    {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        rigidBod = this.GetComponent<Rigidbody>();
    }
    
    void Update ()
    {
        // Determine vector to move character
        Vector3 movementVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBod.velocity = movementVector * -speed;
        rigidBod.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    // Ran only during frames
    void FixedUpdate ()
    {
        // get movementinput from user
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag.Equals("Boundary"))
        {
            Debug.Log("Entered Bound");
            rigidBod.velocity = Vector3.zero;
        }
        
    }
}
