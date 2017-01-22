using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : SpawnableManagerBase
{
    public float TrapY;
    public float TrapZIntervalFromShip;

    public Transform LeftTrapBound, RightTrapBound;

    protected override void StartListeningEvents()
    {
        base.StartListeningEvents();

        GameManager.OnPostGameStart += OnGameStarted;
        GameManager.OnGameOver += OnGameEnded;
        GameManager.OnLevelCompleted += OnGameEnded;
    }

    protected override void FinishListeningEvents()
    {
        base.FinishListeningEvents();

        GameManager.OnPostGameStart -= OnGameStarted;
        GameManager.OnGameOver -= OnGameEnded;
        GameManager.OnLevelCompleted -= OnGameEnded;
    }

    void OnGameStarted()
    {
        StartSpawnProgress();
    }

    void OnGameEnded()
    {
        StopSpawnProgress();
    }

    protected override Vector3 GetSpawnPos()
    {
        Vector3 spawnPos = Vector3.zero;

        spawnPos.x = Utilities.NextFloat(LeftTrapBound.position.x, RightTrapBound.position.x);
        spawnPos.y = TrapY;

        spawnPos.z = ShipController.Instance.transform.position.z + TrapZIntervalFromShip;

        return spawnPos;
    }
}
