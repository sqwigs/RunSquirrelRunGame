using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class SpikeHazard : HazardInterface {

    private static float moveTime = 0.5f;
    private float startTransform, endTransform;

    // Use this for initialization
    void Start () {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        Transform transform = GetComponent<Transform>();
        startTransform = transform.position.y + transform.lossyScale.y;
        endTransform = startTransform - transform.lossyScale.y;

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

			transform.DOMoveY(startTransform, moveTime);

			yield return new WaitForSeconds (trapStaticDelay);

			transform.DOMoveY(endTransform, moveTime);

		}
	}
	
}
