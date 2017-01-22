using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public static Action OnPostGameOverClosed, OnPreGameOverClosed, OnPostGameOverOpened, OnPreGameOverOpened;

    public MMTweenAlpha UIFadeTween;
    public GameObject GameOverUI, GameOverSprite, LevelCompleteSprite;
    public Button RestartButton;

    public static UIGameOver Instance { get; private set; }

    void FireOnPreGameOverOpened()
    {
        if (OnPreGameOverOpened != null)
            OnPreGameOverOpened();
    }

    void FireOnPostGameOverOpened()
    {
        if (OnPostGameOverOpened != null)
            OnPostGameOverOpened();
    }

    void FireOnPreGameOverClosed()
    {
        if (OnPreGameOverClosed != null)
            OnPreGameOverClosed();
    }

    void FireOnPostGameOverClosed()
    {
        if (OnPostGameOverClosed != null)
            OnPostGameOverClosed();
    }

    void Awake()
    {
        Instance = this;
        
        RestartButton.onClick.AddListener(OnRestartButtonPressed);

        GameOverUI.gameObject.SetActive(false);

        StartListeningEvents();
    }

    void OnDestroy()
    {
        StopListeningEvents();
    }

    void StartListeningEvents()
    {
        GameManager.OnGameInitialize += CloseGameOver;
        GameManager.OnGameOver += OpenGameOver;
        GameManager.OnGameClosed += CloseGameOver;
    }

    void StopListeningEvents()
    {
        GameManager.OnGameInitialize -= CloseGameOver;
        GameManager.OnGameOver -= OpenGameOver;
        GameManager.OnGameClosed -= CloseGameOver;
    }

    void OnRestartButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void OpenGameOver()
    {
        switch(GameManager.Instance.CurGameState)
        {
            case GameState.GameOver:
                GameOverSprite.gameObject.SetActive(true);
                LevelCompleteSprite.gameObject.SetActive(false);
                break;
            case GameState.LevelCompleted:
                LevelCompleteSprite.gameObject.SetActive(true);
                GameOverSprite.gameObject.SetActive(false);
                break;
        }

        FireOnPreGameOverOpened();

        GameOverUI.gameObject.SetActive(true);

        FireOnPostGameOverOpened();

        UIFadeTween.PlayForward();
    }

    void CloseGameOver()
    {
        FireOnPreGameOverClosed();
        
        FireOnPostGameOverClosed();

        if (GameOverUI.gameObject.activeSelf)
            UIFadeTween.AddOnFinish(() => GameOverUI.gameObject.SetActive(false), false).PlayReverse();
    }
}
