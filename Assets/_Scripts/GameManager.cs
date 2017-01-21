using System;
using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame
}

public class GameManager : MonoBehaviour
{
    public GameState CurGameState;

    public static Action OnGameInitialize, OnGameOver, OnPreGameStart, OnPostGameStart;

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

        MainMenuController.OnPostMainMenuClosed += FireOnPostGameStart;
    }

    void Start()
    {
        FireOnGameInitialized();
    }

    public void StartGame()
    {
        FireOnPreGameStart();
    }

    public void GameOver()
    {
        FireOnGameOver();
    }

    void CloseGame()
    {

    }
}
