using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BoundaryCollision : MonoBehaviour {


    void Start ()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
    }

    /**
    *  If other enters the boundary, execute event
    */
    void OnCollisionEnter(Collider other)
    {
        if (other.tag.Equals("Boundary"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
