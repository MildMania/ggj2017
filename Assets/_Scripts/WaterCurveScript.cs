using System;
using System.Collections;
using UnityEngine;
using VacuumShaders.CurvedWorld;

public class WaterCurveScript : MonoBehaviour
{
    public CurvedWorld_Controller CurveController;
    public float DisplacementXRadius, DisplacementYRadius;
    public float Cooldown;
    public float MakingLinearDuration;

    float _randomYBend, _randomXBias;
    float _remTime;
    bool _canCurve;
    Vector3 _startingBend, _newbend, _startingBias, _newbias;

    void Awake()
    {
        Initialize();

        StopCurving();

        StartListeningEvents();
    }

    void Initialize()
    {
        _startingBend = CurveController.GetBend();
        _newbend = _startingBend;

        _startingBias = CurveController.GetBias();
        _newbias = _startingBias;

        _remTime = Cooldown;
    }

    void StartListeningEvents()
    {
        GameManager.OnPostGameStart += StartCurving;
    }

    void StopListeningEvents()
    {
        GameManager.OnGameOver -= StopCurving;
    }

    void StartCurving()
    {
        Initialize();

        _canCurve = true;
    }

    void StopCurving()
    {
        _canCurve = false;

        MakeLineLinear();
    }

    void MakeLineLinear()
    {
        StartCoroutine(MakeLineLinearRoutine());
    }

    IEnumerator MakeLineLinearRoutine()
    {
        _startingBend = CurveController.GetBend();
        _startingBias = CurveController.GetBias();

        _newbend = Vector3.zero;
        _newbias = Vector3.zero;

        float passedTime = 0f;

        while (CurveController.GetBend().magnitude != 0 || CurveController.GetBias().magnitude != 0)
        {
            Vector3 tempBend = Vector3.LerpUnclamped(_startingBend, _newbend, passedTime / MakingLinearDuration);
            Vector3 tempBias = Vector3.LerpUnclamped(_startingBias, _newbias, passedTime / MakingLinearDuration);

            CurveController.SetBias(tempBias);
            CurveController.SetBend(tempBend);

            yield return Utilities.WaitForFixedUpdate;
        }
    }

    void Update()
    {
        if (!_canCurve)
            return;

        _remTime -= Time.unscaledDeltaTime;

        Vector3 tempBend = Vector3.LerpUnclamped(_startingBend, _newbend, 1 - (_remTime / Cooldown));
        tempBend.x = CurveController._V_CW_Bend_X;
        tempBend.z = CurveController._V_CW_Bend_Z;

        Vector3 tempBias = Vector3.LerpUnclamped(_startingBias, _newbias, 1 - (_remTime / Cooldown));
        tempBias.y = CurveController._V_CW_Bias_Y;
        tempBias.z = CurveController._V_CW_Bend_Z;

        CurveController.SetBias(tempBias);
        CurveController.SetBend(tempBend);

        if (_remTime > 0)
            return;

        SetRandomCurve();
    }

    void SetRandomCurve()
    {
        _remTime = Cooldown;

        SetRandomValues();

        _newbend = new Vector3(CurveController._V_CW_Bend_X, CurveController._V_CW_Bend_Y + _randomYBend);
        _startingBend = CurveController.GetBend();

        _newbias = new Vector3(CurveController._V_CW_Bias_X + _randomXBias, CurveController._V_CW_Bias_Y);
        _startingBias = CurveController.GetBias();
    }

    void SetRandomValues()
    {
        UnityEngine.Random.InitState((int)Time.realtimeSinceStartup);

        _randomYBend = UnityEngine.Random.Range(-DisplacementYRadius, DisplacementYRadius);
        _randomXBias = UnityEngine.Random.Range(-DisplacementXRadius, DisplacementXRadius);
    }
}
