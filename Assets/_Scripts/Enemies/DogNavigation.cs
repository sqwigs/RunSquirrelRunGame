using UnityEngine;
using System.Collections;
using System;

public class DogNavigation : Navigable
{

    protected override IEnumerator FreezeInPlace()
    {
        _navAgent.Stop();

        yield return new WaitForSeconds(timeFrozen);

        _navAgent.Resume();
    }
}
