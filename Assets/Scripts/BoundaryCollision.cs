﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BoundaryCollision : MonoBehaviour {

	// Runs once at beginning
	void Start ()
	{
		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);
	}

    /**
    *  If other enters the boundary, execute event
    */
	void OnTriggerEnter (Collider other)
    {
		if (other.tag.Equals ("Boundary")) 
		{
			Debug.Log ("Trying to Pause");
			transform.DOPause ();
		}
    }

}
