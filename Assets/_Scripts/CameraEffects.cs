using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffects : MonoBehaviour
{
    //public static Action OnBlurClosed, OnBlurOpened;
    //void FireOnBlurClosed()
    //{
    //    if (OnBlurClosed != null)
    //        OnBlurClosed();
    //}

    //void FireOnBlurOpened()
    //{
    //    if (OnBlurOpened != null)
    //        OnBlurOpened();
    //}

    //public static CameraEffects Instance { get; private set; }

    //public BlurOptimized BlurOptimized;
    //public float BlurTransDur;

    //float _initialBlurSize;

    //void Awake()
    //{
    //    Instance = this;

    //    _initialBlurSize = BlurOptimized.blurSize;
    //}

    //public void OpenBlur()
    //{
    //    StopCoroutine(CloseBlurRoutine());
    //    StartCoroutine(OpenBlurRoutine());
    //}

    //public void CloseBlur()
    //{
    //    StopCoroutine(OpenBlurRoutine());
    //    StartCoroutine(CloseBlurRoutine());
    //}

    //IEnumerator OpenBlurRoutine()
    //{
    //    float passedTime = 0;

    //    while (passedTime <= BlurTransDur)
    //    {
    //        BlurOptimized.blurSize = _initialBlurSize * passedTime;

    //        yield return null;

    //        passedTime += Time.unscaledDeltaTime;
    //    }

    //    BlurOptimized.blurSize = _initialBlurSize;

    //    FireOnBlurOpened();
    //}

    //IEnumerator CloseBlurRoutine()
    //{
    //    float passedTime = BlurTransDur;

    //    while (passedTime >= 0)
    //    {
    //        BlurOptimized.blurSize = _initialBlurSize * passedTime;

    //        yield return null;

    //        passedTime -= Time.unscaledDeltaTime;
    //    }

    //    BlurOptimized.blurSize = 0;

    //    FireOnBlurClosed();
    //}
}
