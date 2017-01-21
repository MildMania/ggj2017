using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapBase : SpawnableBase
{
    public float FloatAmount;
    public float FloatDuration;

    public Rigidbody Rigidbody;

    public override void Activate(Vector3 spawnPos, params object[] list)
    {
        base.Activate(spawnPos, list);

        ResetRigidbody();

        float newY = transform.localPosition.y + FloatAmount;

        transform.DOLocalMoveY(newY, FloatDuration).SetEase(Ease.OutBounce);
    }

    void ResetRigidbody()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
    }

}
