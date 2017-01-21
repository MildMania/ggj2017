using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform TargetToFollow;
    public float SmoothTime;

    Vector3 _currentVelocity;

    public void Awake()
    {
        StartCoroutine(StartFollow());
    }

    IEnumerator StartFollow()
    {
        while(true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, TargetToFollow.position, ref _currentVelocity, SmoothTime);

            yield return Utilities.WaitForFixedUpdate;
        }
    }
}
