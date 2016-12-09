using UnityEngine;
using System.Collections;

public abstract class HazardInterface : MonoBehaviour
{
    // represents the time when hazard resets position
    public int trapStaticDelay;
    public float triggerTime, moveTime;
	public float startY, endY;

	protected abstract IEnumerator trapControl();	  
}
