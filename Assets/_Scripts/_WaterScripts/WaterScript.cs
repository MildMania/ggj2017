using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public Rigidbody ShipRigidbody;

    public float FlowForce;

    Vector3 _flowDirection = new Vector3(0, 0, 1);

    public float ResistanceTorqueTreshold;
    public float ResistanceTorque;

    Vector3 _resistanceTorqueDir;

    IEnumerator _flowRoutine;

    private void Awake()
    {
        StartFlowProgress();
    }

    void StartFlowProgress()
    {
        _flowRoutine = FlowProgress();
        StartCoroutine(_flowRoutine);
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
}
