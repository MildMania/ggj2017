using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static partial class Utilities
{
    public static YieldInstruction WaitForFixedUpdate;
    public static YieldInstruction WaitForEndOfFrame;

    public static void Initialize()
    {
        WaitForFixedUpdate = new UnityEngine.WaitForFixedUpdate();
        WaitForEndOfFrame = new UnityEngine.WaitForEndOfFrame();

        InitializeMath();
    }
}
