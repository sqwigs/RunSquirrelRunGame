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
	
    /// <summary>
    /// Health is decreased by removing orb and switching shield image. 
    /// </summary>
	public void healthDecrease ()
    {
        shieldUIControl.degradeShield();
        healthOrbUIControl.removeOrb();
    }
}
