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
    public float MaxZSpeed;

    public float MaxZRot;

    public Transform LeftAnchor;
    public Transform RightAnchor;

    public float PushForce;
    Vector3 _pushForceDirection = new Vector3(1, 0, 0);

    IEnumerator _checkInputRoutine;

    private void Awake()
    {
        _instance = this;

        StartListeningEvents();
    }

    private void OnDestroy()
    {
        _instance = null;

        FinishListeningEvents();
    }

    protected void StartListeningEvents()
    {
        GameManager.OnPostGameStart += OnGameStarted;
    }

    protected void FinishListeningEvents()
    {
        GameManager.OnPostGameStart -= OnGameStarted;
    }

    void OnGameStarted()
    {
        Debug.Log("on game started");
        StartCheckInputProgress();
    }

    void StartCheckInputProgress()
    {
        StopCheckInputProgress();

        _checkInputRoutine = CheckInputProgress();
        StartCoroutine(_checkInputRoutine);
    }

    void StopCheckInputProgress()
    {
        if (_checkInputRoutine != null)
            StopCoroutine(_checkInputRoutine);
    }

    IEnumerator CheckInputProgress()
    {
        while (true)
        {
            CheckInput();
            yield return null;
        }
    }

    private void Update()
    {
        LimitZSpeed();
        LimitZRot();
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
    
    void LimitZSpeed()
    {
        Vector3 vel = Rigidbody.velocity;

        if (vel.z > MaxZSpeed)
            vel.z = MaxZSpeed;

        Rigidbody.velocity = vel;

    }

    void LimitZRot()
    {
        Vector3 rot = transform.localEulerAngles;

        if (rot.z > MaxZRot 
            && rot.z < 180)
            rot.z = MaxZRot;
        else if (rot.z > 180
            && rot.z < 360 - MaxZRot)
            rot.z = 360 - MaxZRot;
        else if (rot.z < -MaxZRot)
            rot.z = -MaxZRot;

        transform.localEulerAngles = rot;
    }
}
