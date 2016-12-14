using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthOrbControl : MonoBehaviour {

    private List<Image> orbs;
    private int numOfOrbs;
    private int index;

	// Use this for initialization
	void Start () {
        orbs = new List<Image>();
        foreach (Transform child in transform)
        {
            foreach(Transform grandchild in child)
            {
                orbs.Add(grandchild.gameObject.GetComponent<Image>());
                numOfOrbs++;
            }
        }

        index = 0;
    }
	
    /// <summary>
    /// Will remove Health Orb from GUI
    /// </summary>
	public void removeOrb ()
    {
            orbs[index].enabled = false;
            index = (index + 1) % numOfOrbs; 
    }

    /// <summary>
    /// Take back player to full health on GUI
    /// </summary>
    public void healToFull()
    {
       foreach (Image orb in orbs)
        {
            orb.enabled = true;
        }
        index = 0;
    }
}
