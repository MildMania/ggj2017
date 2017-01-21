using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionEnum
{
    Left,
    Right,
}

public class ShipController : MonoBehaviour
{
    static ShipController _instance;

    public static ShipController Instance { get { return _instance; } }

    public Rigidbody Rigidbody;

    public Transform LeftAnchor;
    public Transform RightAnchor;

    public float PushForce;
    Vector3 _pushForceDirection = new Vector3(1, 0, 0);

    private void Awake()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private void Update()
    {
        CheckInput();
        UpdateRotation();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            PushShip(DirectionEnum.Left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            PushShip(DirectionEnum.Right);
    }

    void PushShip(DirectionEnum direction)
    {
        Transform targetTR = LeftAnchor;

        float x = 1.0f;

        if (direction == DirectionEnum.Left)
        {
            targetTR = RightAnchor;
            x = -1.0f;
        }

        _pushForceDirection.x = x;

        Rigidbody.AddForceAtPosition(_pushForceDirection * PushForce, targetTR.position, ForceMode.Acceleration);
    }

    void UpdateRotation()
    {
        Vector3 eulerAngles = transform.localEulerAngles;
        eulerAngles.x = 0.0f;

        transform.localEulerAngles = eulerAngles;
    }
}
