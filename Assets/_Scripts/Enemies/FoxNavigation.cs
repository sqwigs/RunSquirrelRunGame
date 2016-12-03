using UnityEngine;
using System.Collections;
using System;

public class FoxNavigation : Navigable
{

    protected override IEnumerator FreezeInPlace()
    {
        yield return null;
    }


}
