using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class SpikeHazard : HazardInterface {

    private static float moveTime = 1;
    private Transform transform;

    // Use this for initialization
    void Start () {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        transform = GetComponent<Transform>();
		StartCoroutine (trapControl () );
        
	}

    /// <summary>
    /// Make trap rise and fall over time of level
    /// </summary>
    /// <returns></returns>
	protected override IEnumerator trapControl() 
	{
		while (true) {
			
			yield return new WaitForSeconds (triggerTime);

			transform.DOMoveY(transform.position.y + transform.lossyScale.y, moveTime);

			yield return new WaitForSeconds (trapStaticDelay);

			transform.DOMoveY(transform.position.y - transform.lossyScale.y, moveTime);

		}
	}
	
}
