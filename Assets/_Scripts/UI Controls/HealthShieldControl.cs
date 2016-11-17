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
	
    public void degradeShield ()
    {
        if (shields.Count > 1)
        {
            GameObject lostHealth = shields.Dequeue();
            lostHealth.SetActive(false);
            shields.Peek().SetActive(true);
        }
        
    }
}
