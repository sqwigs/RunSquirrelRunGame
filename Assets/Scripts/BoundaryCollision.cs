﻿using UnityEngine;
using System.Collections;

public class BoundaryCollision : MonoBehaviour {

    /**
    *  If other enters the boundary, execute event
    */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Boundary"))
        {
            Debug.Log("Entered Bound");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
