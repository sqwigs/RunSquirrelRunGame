using UnityEngine;
using System.Collections;
using System;

public class WolfNavigation : Navigable {

    public GameObject zoneObject;

    public override void Start()
    {
        base.Start();

        _navAgent.autoBraking = false;
    }

    public override void TargetFound(Vector3 lastKnownPos)
    {
        if (!targetFound )
            _navAgent.speed *= 2;

        base.TargetFound(lastKnownPos);   
    }

    public override void TargetLost (Vector3 lastKnownPos)
    {
        if (targetFound)
            _navAgent.speed /= 2;

        base.TargetLost(lastKnownPos);
    }

    protected override IEnumerator FreezeInPlace()
    {
        _navAgent.Stop();

        yield return new WaitForSeconds(timeFrozen);

        _navAgent.Resume();
    }
}
