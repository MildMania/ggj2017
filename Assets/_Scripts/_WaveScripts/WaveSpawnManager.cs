using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnManager : SpawnableManagerBase
{
    public Transform LeftWavePH, RightWavePH;

    public float WaveSpeed;

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

        SpawnSpawnable(GetSpawnPos(), _waveDirection, WaveSpeed);
    }

    protected override Vector3 GetSpawnPos()
    {
        Vector3 spawnPos = Vector3.zero;

        if (_waveDirection == DirectionEnum.Left)
            spawnPos.x = RightWavePH.position.x;
        else
            spawnPos.x = LeftWavePH.position.x;
        spawnPos.y = transform.position.y;

        float shipPercInRiver = WaterScript.Instance.GetShipWidthPerc();

        float curZInterval = ZIntervalFromShip;

        if (_waveDirection == DirectionEnum.Right)
            curZInterval *= shipPercInRiver;
        else
            curZInterval *= (1.0f - shipPercInRiver);

        spawnPos.z = ShipController.Instance.transform.position.z + curZInterval;

        return spawnPos;

    }
}
