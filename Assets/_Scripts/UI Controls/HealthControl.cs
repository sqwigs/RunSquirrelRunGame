using UnityEngine;
using System.Collections;


public class HealthControl : MonoBehaviour {

    private HealthShieldControl shieldUIControl;
    private HealthOrbControl healthOrbUIControl;

	// Use this for initialization
	void Start () {
        shieldUIControl = this.GetComponentInChildren<HealthShieldControl>();
        healthOrbUIControl = this.GetComponentInChildren<HealthOrbControl>();
	}
	
	public void healthUpdate ()
    {
        shieldUIControl.degradeShield();
        healthOrbUIControl.removeOrb();
    }
}
