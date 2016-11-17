using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthOrbControl : MonoBehaviour {

    private Stack<Image> orbs;

	// Use this for initialization
	void Start () {
        orbs = new Stack<Image>();
        foreach (Transform child in transform)
        {
            foreach(Transform grandchild in child)
            {
                orbs.Push(grandchild.gameObject.GetComponent<Image>());
            }
        }
    }
	
	public void removeOrb ()
    {
        if (orbs.Count > 0)
        {
            orbs.Pop().enabled = false;
        }
    }
}
