using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableWave : SpawnableBase
{
    public Rigidbody Rigidbody;

    DirectionEnum _waveDirection;
    float _speed;

    public override void Activate(Vector3 spawnPos, params object[] list)
    {
        _waveDirection = (DirectionEnum)list[0];
        _speed = (float)list[1];

        base.Activate(spawnPos, list);

        SetLocalScale();
        ResetRigidbody();
        StartAttackProgress();
    }

    void SetLocalScale()
    {
        if (_waveDirection == DirectionEnum.Right)
            transform.localScale = new Vector3(-30, 30, 30);
        else
            transform.localScale = new Vector3(30, 30, 30);
    }

    void ResetRigidbody()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
    }

    void StartAttackProgress()
    {
        Vector3 moveDirection = new Vector3(1, 0, 0);

        if (_waveDirection == DirectionEnum.Left)
            moveDirection.x = -1.0f;

        Rigidbody.velocity = moveDirection * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)LayerEnum.Ship)
            Deactivate();

    }
}
