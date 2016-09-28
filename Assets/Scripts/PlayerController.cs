using UnityEngine;
using System.Collections;

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
        charControl = this.GetComponent<CharacterController>();
        rigidBod = this.GetComponent<Rigidbody>();
    }
    
    void Update ()
    {
        
        // Determine vector to move character
        Vector3 movementVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBod.velocity = movementVector * speed;

        this.rigidBod.position = new Vector3
        (
            Mathf.Clamp(rigidBod.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBod.position.z, boundary.zMin, boundary.zMax)
        );

        rigidBod.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    // Ran only during frames
    void FixedUpdate ()
    {
        // get movementinput from user
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        // Restricts movement to Horizontal or Vertical, Disabling Diagonal.
        // TODO: Fix bug were you hold both up and side causing weird direction turn. 
        if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
        {
            moveVertical = 0.0f;
        }
        else
        {
            moveHorizontal = 0.0f;
        }
    }
}
