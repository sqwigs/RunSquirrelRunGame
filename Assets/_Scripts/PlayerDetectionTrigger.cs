using UnityEngine;
using System.Collections;

public class PlayerDetectionTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            other.GetComponent<Navigable>().Freeze();
        }
    }

}
