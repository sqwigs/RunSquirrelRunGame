using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

/// <summary>
/// This interface is to be inherited by all enemies, 
/// thus allowing for a consistent enemy implementation
/// </summary>
public abstract class EnemyInterface : MonoBehaviour { 
    // public movement variables for Enemies
	public float speed;
    public float moveWait;
    public float pauseWait;
    public Boundary boundary; 

    // Control system for enemy types
    protected new Transform transform;
    protected Vector3 destination;

   /// <summary>
   /// Movement control for enemy types must be implemented abstractly
   /// </summary>
    protected abstract void movement ();


}
