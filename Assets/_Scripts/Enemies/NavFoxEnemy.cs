using UnityEngine;
using System.Collections;
using System;

public class NavFoxEnemy : Navigable
{

    protected override IEnumerator FreezeInPlace()
    {
        yield return null;
    }


}
