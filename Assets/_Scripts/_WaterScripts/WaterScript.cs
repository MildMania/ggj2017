﻿using System.Collections;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    static WaterScript _instance;

    public static WaterScript Instance { get { return _instance; } }

    public Transform LeftBound, RightBound;

    float _totalRiverWidth;

    public Rigidbody ShipRigidbody;

    public float FlowForce;

    Vector3 _flowDirection = new Vector3(0, 0, 1);

    public float ResistanceTorqueTreshold;
    public float ResistanceTorque;

    Vector3 _resistanceTorqueDir;

    IEnumerator _flowRoutine;

    private void Awake()
    {
        _instance = this;

        InitTotalWidth();

        StartListeningEvents();

        StartFlowProgress();
    }

    private void OnDestroy()
    {
        FinishListeningEvents();
    }

    void StartListeningEvents()
    {
        GameManager.OnGameOver += OnGameEnded;
    }

    void FinishListeningEvents()
    {
        GameManager.OnGameOver -= OnGameEnded;
    }

    void OnGameEnded()
    {
        StopFlowProgress();
    }

    void InitTotalWidth()
    {
        _totalRiverWidth = RightBound.position.x - LeftBound.position.x;
    }

    void StartFlowProgress()
    {
        StopFlowProgress();

        _flowRoutine = FlowProgress();
        StartCoroutine(_flowRoutine);
    }

    void StopFlowProgress()
    {
        if (_flowRoutine != null)
            StopCoroutine(_flowRoutine);
    }

    IEnumerator FlowProgress()
    {
        while(true)
        {
            ApplyFlowForce();
            ApplyFlowResistanceTorque();
            yield return new WaitForFixedUpdate();
        }
    }

    void ApplyFlowForce()
    {
        ShipRigidbody.AddForce(FlowForce * _flowDirection);
    }

    void ApplyFlowResistanceTorque()
    {
        _resistanceTorqueDir = Vector3.zero;

        Vector3 shipEuler = ShipRigidbody.transform.localEulerAngles;

        if(shipEuler.z > ResistanceTorqueTreshold 
            && shipEuler.z < (360.0f - ResistanceTorqueTreshold))
        {
            if (shipEuler.z < 180.0f)
                _resistanceTorqueDir.z = -1.0f;
            else
                _resistanceTorqueDir.z = 1.0f;
        }

        ShipRigidbody.AddTorque(_resistanceTorqueDir * ResistanceTorque);

    }

    public float GetShipWidthPerc()
    {
        float perc = (ShipRigidbody.transform.position.x - LeftBound.position.x) / _totalRiverWidth;

        return perc;
    }
}
