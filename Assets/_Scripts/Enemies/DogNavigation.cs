using UnityEngine;
using System.Collections;
using System;

public class DogNavigation : Navigable
{

    private GameObject freezeEffect;
    public override void Start()
    {
        base.Start();

        if (!GetChild(this.gameObject, "FreezeEffect", out freezeEffect))
        {
            DebugMessage("Freeze Effect");
        }
    }

    public override void TargetFound(Vector3 curPos)
    {
        if (!targetFound)
        {
            _navAgent.speed *= 2;
        }
        base.TargetFound(curPos);
        
    }

    public override void TargetLost(Vector3 lastKnownPos)
    {
        if (targetFound)
        {
            _navAgent.speed /= 2;
        }
        base.TargetLost(lastKnownPos);
    }

    protected override IEnumerator FreezeInPlace()
    {
        _navAgent.Stop();

        freezeEffect.SetActive(true);

        yield return new WaitForSeconds(timeFrozen);

        freezeEffect.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        _navAgent.Resume();
    }
}
