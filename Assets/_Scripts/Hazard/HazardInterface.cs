using UnityEngine;
using System.Collections;

public abstract class HazardInterface : MonoBehaviour
{
    // represents the time when hazard resets position
    public int resetTime;
    public int trapStaticDelay;
    public float triggerTime;

	protected abstract IEnumerator trapControl();

//    /// <summary>
//    /// This function represents what the trap will do when it is time to spring the trap.
//    /// </summary>
//    protected abstract void activateTrap();
//
//    /// <summary>
//    /// This function represents what the trap will do when it is time to reset it to its' active state. 
//    /// </summary>
//    protected abstract void deactivateTrap();
//
//    /// <summary>
//    /// Will increment a timer until the timer exceeds the established triggerTime
//    /// </summary>
//    /// <returns> If the trap is triggered, will return true. Otherwise it will return false. </returns>
//    protected bool trapTimer ()
//    {    
//        if (!trapActivated && currTime > triggerTime)
//        {
//            activateTrap();
//            currTime = 0;
//            trapActivated = true;
//
//            // activate a threading timer to reset the trap and then have it remain active. 
//           // new System.Threading.Timer((obj) => { deactivateTrap();  trapActivated = false; }, null, trapStaticDelay, resetTime);
//
//            return true;
//        }
//
//        currTime += Time.deltaTime;
//
//        return false; 
//    }


	  
}
