using System;
using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState CurGameState;

    public static Action OnGameInitialize, OnGameOver, OnPreGameStart, OnPostGameStart, OnGameClosed;

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

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        MainMenuController.OnPostMainMenuClosed += PostStartGame;
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

    public void CloseGame()
    {
        CurGameState = GameState.MainMenu;

        FireOnGameClosed();
    }
}
