using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyCollision : MonoBehaviour
{

    // Runs once at beginning
    void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
    }

    /**
    *  If other enters the boundary, execute event
    */
    void OnCollisionEnter(Collision other)
    {
          transform.DOPause();
    }

}
