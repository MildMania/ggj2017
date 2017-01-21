using System;
using System.Collections;
using UnityEngine;
using VacuumShaders.CurvedWorld;

public class WaterCurveScript : MonoBehaviour
{
    public CurvedWorld_Controller CurveController;
    public float DisplacementXRadius, DisplacementYRadius;
    public float Cooldown;
    float _randomYBend, _randomXBias;

    float _remTime;

    Vector3 _startingBend, _newbend, _startingBias, _newbias;

    void Awake()
    {
        _startingBend = CurveController.GetBend();
        _newbend = _startingBend;

        _startingBias = CurveController.GetBias();
        _newbias = _startingBias;

        _remTime = Cooldown;
    }

    void Update()
    {
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
