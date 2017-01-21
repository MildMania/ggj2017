using UnityEngine;
using System.Collections;
using System;

public abstract class SpawnableBase : MonoBehaviour
{
    public Renderer Renderer;

    protected SpawnableManagerBase _parentManager;

    protected bool _enteredScreen;

    IEnumerator _checkAutoDeactivateRoutine;

    const float DEACTIVATE_Z_INTERVAL = 50.0f;

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

        StartCheckAutoDeactivateProgress();
    }

    void StartCheckAutoDeactivateProgress()
    {
        StopCheckAutoDeactivateProgress();

        _checkAutoDeactivateRoutine = CheckAutoDeactivate();
        StartCoroutine(_checkAutoDeactivateRoutine);
    }

    void StopCheckAutoDeactivateProgress()
    {
        if (_checkAutoDeactivateRoutine != null)
            StopCoroutine(_checkAutoDeactivateRoutine);
    }

    protected virtual IEnumerator CheckAutoDeactivate()
    {
        while (transform.position.z > ShipController.Instance.transform.position.z - DEACTIVATE_Z_INTERVAL)
            yield return null;

        Deactivate();
    }

    public virtual void Deactivate()
    {
        transform.parent = _parentManager.SpawnableCarrier.transform;

        _parentManager.SpawnableDeactivated(this);

        gameObject.SetActive(false);
    }

    protected void SetRendererActive(bool isActive)
    {
        if(Renderer != null)
            Renderer.enabled = isActive;
    }
}