using UnityEngine;
using System.Collections;
using System;

public class NavigationScript : EnemyInterface {

    private Transform agent;

	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<NavMeshAgent>().setDestination(agent);
	}
}
