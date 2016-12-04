using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthShieldControl : MonoBehaviour {

    private Queue<GameObject> shields;
    
    // Use this for initialization
	void Awake () {
        shields = new Queue<GameObject>();
        foreach (Transform child in transform)
        {
            shields.Enqueue(child.gameObject);
        }
	}

    public void degradeShield()
    {
        GameObject prevShield = shields.Dequeue();
        prevShield.SetActive(false); // flip state of current shield
        shields.Peek().SetActive(true);
        shields.Enqueue(prevShield);
    }
}
