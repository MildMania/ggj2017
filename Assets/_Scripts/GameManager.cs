using System;
using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame,
    GameOver,
    LevelCompleted,
}

public class GameManager : MonoBehaviour
{
    public GameState CurGameState;

    #region Events
    public static Action OnGameInitialize, OnGameOver, OnPreGameStart, OnPostGameStart, OnGameClosed, OnLevelCompleted;

    void FireOnGameInitialized()
    {
        if (OnGameInitialize != null)
            OnGameInitialize();
    }

    void FireOnGameOver()
    {
        if (OnGameOver != null)
            OnGameOver();
    }

    void FireOnLevelCompleted()
    {
        if (OnLevelCompleted != null)
            OnLevelCompleted();
    }

    void FireOnGameClosed()
    {
        if (OnGameClosed != null)
            OnGameClosed();
    }

    void FireOnPreGameStart()
    {
        if (OnPreGameStart != null)
            OnPreGameStart();
    }

    void FireOnPostGameStart()
    {
        if (OnPostGameStart != null)
            OnPostGameStart();
    }
    #endregion

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        MainMenuController.OnPostMainMenuClosed += PostStartGame;
    }

    private void OnDestroy()
    {
        MainMenuController.OnPostMainMenuClosed -= PostStartGame;
    }

    void Start()
    {
        CurGameState = GameState.MainMenu;

        FireOnGameInitialized();
    }

    public void StartGame()
    {
        FireOnPreGameStart();
    }

    public void PostStartGame()
    {
        CurGameState = GameState.InGame;

        FireOnPostGameStart();
    }

    public void GameOver()
    {
        CurGameState = GameState.GameOver;

        FireOnGameOver();
    }

    public void LevelCompleted()
    {
        CurGameState = GameState.LevelCompleted;

        FireOnLevelCompleted();
    }

    void CloseGame()
    {
        CurGameState = GameState.MainMenu;

        FireOnGameClosed();
    }
}
