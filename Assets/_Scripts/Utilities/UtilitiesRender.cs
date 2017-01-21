using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Fade
{
    In,
    Out
}


static partial class Utilities
{
    public static bool IsVisibleFrom(Renderer renderer, Plane[] planes)
    {
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    public static IEnumerator WaitForParticlePlay(ParticleSystem particleSystem, Action<ParticleSystem> callback)
    {
        while (particleSystem.isPlaying)
            yield return null;

        callback(particleSystem);
    }
}
