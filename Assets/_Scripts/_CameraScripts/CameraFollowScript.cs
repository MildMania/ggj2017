using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform TargetToFollow;
    public float SmoothTime;

    Vector3 _currentVelocity;

    IEnumerator _followRoutine;

    public void Awake()
    {
        StartListeningEvents();
        StartFollowProgress();
    }

    private void OnDestroy()
    {
        FinishListeningEvents();
    }

    void StartListeningEvents()
    {
        GameManager.OnGameOver += OnGameOver;
    }

    void FinishListeningEvents()
    {
        GameManager.OnGameOver -= OnGameOver;
    }

    void OnGameOver()
    {
        StopFollowProgress();
    }

    void StartFollowProgress()
    {
        StopFollowProgress();

        _followRoutine = FollowProgress();
        StartCoroutine(_followRoutine);
    }

    void StopFollowProgress()
    {
        if (_followRoutine != null)
            StopCoroutine(_followRoutine);
    }

    IEnumerator FollowProgress()
    {
        while(true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, TargetToFollow.position, ref _currentVelocity, SmoothTime);

            yield return Utilities.WaitForFixedUpdate;
        }
    }
}
