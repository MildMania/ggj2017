using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float LevelDuration;

    public float RemLevelDuration { get; private set; }

    IEnumerator _levelRoutine;

    private void Awake()
    {
        StartListeningEvents();
    }

    private void OnDestroy()
    {
        FinishListeningEvents();
    }

    void StartListeningEvents()
    {
        GameManager.OnPostGameStart += OnGameStarted;
        GameManager.OnGameOver += OnGameEnded;
    }

    void FinishListeningEvents()
    {
        GameManager.OnPostGameStart -= OnGameStarted;
        GameManager.OnGameOver -= OnGameEnded;
    }

    void OnGameStarted()
    {
        StartLevelProgress();
    }

    void OnGameEnded()
    {
        StopLevelProgress();
    }

    void StartLevelProgress()
    {
        _levelRoutine = LevelProgress();
        StartCoroutine(_levelRoutine);
    }

    void StopLevelProgress()
    {
        if (_levelRoutine != null)
            StopCoroutine(_levelRoutine);
    }

    IEnumerator LevelProgress()
    {
        RemLevelDuration = LevelDuration;

        while(RemLevelDuration > 0)
        {
            RemLevelDuration -= Time.fixedDeltaTime;

            yield return Utilities.WaitForFixedUpdate;
        }

        LevelCompleted();
    }

    void LevelCompleted()
    {
        GameManager.Instance.LevelCompleted();
    }


}
