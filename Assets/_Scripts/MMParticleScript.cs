using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MMParticleScript : MonoBehaviour
{
    public bool StickToParent;

    Transform _particleParent;
    ParticleSystem _particleSystem;

    public ParticleSystem ParticleSystem { get { return _particleSystem; } }

    Vector3 _initPos;

    #region Events

    public Action<MMParticleScript> OnComplete;

    void FireOnComplete()
    {
        if (OnComplete != null)
            OnComplete(this);
    }

    #endregion

    void Awake()
    {
        InitParent();
        InitParticleSystem();
        InitParticleLocalPos();
    }

    void InitParent()
    {
        _particleParent = transform.parent;
    }

    void InitParticleSystem()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void InitParticleLocalPos()
    {
        _initPos = transform.localPosition;
    }

    public void Play()
    {
        transform.parent = _particleParent;
        transform.localPosition = _initPos;

        if (!StickToParent)
            transform.parent = null;

        gameObject.SetActive(true);
        _particleSystem.Play();

        StartCoroutine(Utilities.WaitForParticlePlay(_particleSystem, OnParticleFinished));
    }

    public void OnParticleFinished(ParticleSystem ps)
    {
        transform.parent = _particleParent;
        transform.localPosition = _initPos;
        gameObject.SetActive(false);

        FireOnComplete();
    }
}