using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class SpikeHazard : HazardInterface {

    public float moveTime;
    private Transform transform;

    // Use this for initialization
    void Start () {
        DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        transform = GetComponent<Transform>();
        currTime = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (!trapActivated)
        {
            trapTimer();
        }
    }

    protected override void activateTrap()
    {
        transform.DOMoveY(transform.position.y + transform.lossyScale.y, moveTime);
        Debug.Log("In activateTrap()");
    }

    protected override void deactivateTrap()
    {
        transform.DOMoveY(transform.position.y - transform.lossyScale.y, moveTime);
        Debug.Log("In deactivateTrap()");

    }
}
