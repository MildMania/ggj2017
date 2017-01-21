using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnManager : SpawnableManagerBase
{
    public Transform LeftWavePH, RightWavePH;

    public float ZIntervalFromShip;

    DirectionEnum _waveDirection;

    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            CreateWave(DirectionEnum.Right);

        if (Input.GetKeyDown(KeyCode.D))
            CreateWave(DirectionEnum.Left);
    }

    void CreateWave(DirectionEnum direction)
    {
        _waveDirection = direction;

        SpawnSpawnable(GetSpawnPos());
    }

   protected override Vector3 GetSpawnPos()
    {
        Vector3 spawnPos = Vector3.zero;

        if (_waveDirection == DirectionEnum.Left)
            spawnPos.x = LeftWavePH.position.x;
        else
            spawnPos.x = RightWavePH.position.x;

        spawnPos.z = ShipController.Instance.transform.position.z + ZIntervalFromShip;

        return spawnPos;

    }
}
