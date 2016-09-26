using UnityEngine;
using System.Collections;

//[System.Serializable]
//public class Boundary
//{
//    public float xMin, xMax, zMin, zMax;
//}

public class PlayerController : MonoBehaviour
{
    // Player Movement Control Variables
    public float speed = 10;
    // public Boundary bound;

    // Used for movement control
    private CharacterController charControl;

    void Start ()
    {
        charControl = this.GetComponent<CharacterController>();
    }
    
	void Update ()
    {
        float moveHorizontal, moveVertical;
        
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
       

        // Determine vector to move character
        Vector3 movementVector = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move character along movementVector direction at the speed given
        charControl.SimpleMove(movementVector * -speed);

        this.transform.LookAt(this.transform.position + movementVector);
     }
}
