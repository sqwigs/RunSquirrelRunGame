using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class SpikeHazard : HazardInterface {

    // Use this for initialization
    void Start () {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        Transform transform = GetComponent<Transform>();

        StartCoroutine(trapControl () );     
	}

    void OnDestroy ()
    {
        DOTween.Clear();
    }

    /// <summary>
    /// Make trap rise and fall over time of level
    /// </summary>
    /// <returns></returns>
	protected override IEnumerator trapControl() 
	{
		while (true) {
			
			yield return new WaitForSeconds (triggerTime);

			transform.DOMoveY(startY, moveTime);

			yield return new WaitForSeconds (trapStaticDelay);

			transform.DOMoveY(endY, moveTime);

		}
	}
	
}
