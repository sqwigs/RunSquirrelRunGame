using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthShieldControl : MonoBehaviour {

    private List<GameObject> shields;
    private int shieldStates = 0;
    private int index;
    
    // Use this for initialization
	void Awake () {
        shields = new List<GameObject>();
        foreach (Transform child in transform)
        {
            shields.Add(child.gameObject);
            shieldStates++;
        }
        index = 0;
	}

    public void degradeShield()
    {
        shields[index].SetActive(false);
        index = (index + 1) % shieldStates;
        shields[index].SetActive(true);
    }

    public void healToFull()
    {
        foreach (GameObject shield in shields)
        {
            shield.SetActive(false);
        }

        shields[0].SetActive(true);
        index = 0;
    }
}
