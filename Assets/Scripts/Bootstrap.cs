using PrimeTween;
using UnityEngine;

public static class Bootstrap
{
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        PrimeTweenConfig.warnEndValueEqualsCurrent = false;
    }
}