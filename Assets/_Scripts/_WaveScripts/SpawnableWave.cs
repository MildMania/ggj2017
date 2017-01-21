using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableWave : SpawnableBase
{
    public Rigidbody Rigidbody;
    public float WaveForce;

    IEnumerator _attackShipRoutine;

    public override void Activate(Vector3 spawnPos, params object[] list)
    {
        base.Activate(spawnPos, list);

        ResetRigidbody();
        StartAttackProgress();
    }

    void ResetRigidbody()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
    }

    void StartAttackProgress()
    {
        StopAttackProgress();

        _attackShipRoutine = AttackProgress();
        StartCoroutine(_attackShipRoutine);
    }

    void StopAttackProgress()
    {
        if (_attackShipRoutine != null)
            StopCoroutine(_attackShipRoutine);
    }

    IEnumerator AttackProgress()
    {
        Vector3 moveDirection = new Vector3(1, 0, 0);

        if (transform.position.x > 0)
            moveDirection.x = -1.0f;

        while (true)
        {
            Rigidbody.AddForce(moveDirection * WaveForce);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)LayerEnum.Ship)
            Deactivate();

    }
}
