using UnityEngine;
using System.Collections;
using System;

public abstract class SpawnableBase : MonoBehaviour
{
    public Renderer Renderer;

    protected SpawnableManagerBase _parentManager;

    protected bool _enteredScreen;

    IEnumerator _checkAutoDeactivateRoutine;

    public virtual void InitSpawnable(SpawnableManagerBase parentManager)
    {
        _parentManager = parentManager;

        Deactivate();
    }

    public virtual void Activate(Vector3 spawnPos, params object[] list)
    {
        transform.position = spawnPos;

        transform.parent = null;

        SetRendererActive(true);

        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        transform.parent = _parentManager.SpawnableCarrier.transform;

        _parentManager.SpawnableDeactivated(this);

        gameObject.SetActive(false);
    }

    protected void SetRendererActive(bool isActive)
    {
        Renderer.enabled = isActive;
    }
}