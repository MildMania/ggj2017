using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static Action OnPostMainMenuClosed, OnPreMainMenuClosed, OnPostMainMenuOpened, OnPreMainMenuOpened;

    public MMTweenAlpha UIFadeTween;

    public Button PlayButton;

    public static MainMenuController Instance { get; private set; }

    void FireOnPreMainMenuOpened()
    {
        if (OnPreMainMenuOpened != null)
            OnPreMainMenuOpened();
    }

    void FireOnPostMainMenuOpened()
    {
        if (OnPostMainMenuOpened != null)
            OnPostMainMenuOpened();
    }

    void FireOnPreMainMenuClosed()
    {
        if (OnPreMainMenuClosed != null)
            OnPreMainMenuClosed();
    }

    void FireOnPostMainMenuClosed()
    {
        if (OnPostMainMenuClosed != null)
            OnPostMainMenuClosed();
    }

    void Awake()
    {
        Instance = this;

        GameManager.OnGameInitialize += OpenMainMenu;
        GameManager.OnPreGameStart += CloseMainMenu;
        GameManager.OnGameOver += OpenMainMenu;

        PlayButton.onClick.AddListener(OnPlayButtonPressed);
    }

    void OnPlayButtonPressed()
    {
        SceneManager.Instance.PlayPressed();
    }

    void OpenMainMenu()
    {
        FireOnPreMainMenuOpened();

        UIFadeTween.gameObject.SetActive(true);

        PlayButton.gameObject.SetActive(true);
        FireOnPostMainMenuOpened();
        //CameraEffects.OnBlurOpened += OnBlurOpened;
        //CameraEffects.Instance.OpenBlur();

        UIFadeTween.PlayForward();
    }

    void CloseMainMenu()
    {
        PlayButton.gameObject.SetActive(false);

        FireOnPreMainMenuClosed();

        //UIFadeTween.gameObject.SetActive(false);
        FireOnPostMainMenuClosed();
        //CameraEffects.OnBlurClosed += OnBlurClosed;
        //CameraEffects.Instance.CloseBlur();

        UIFadeTween.AddOnFinish(() => UIFadeTween.gameObject.SetActive(false), false).PlayReverse();
    }

    //void OnBlurOpened()
    //{
    //    CameraEffects.OnBlurOpened -= OnBlurOpened;
    //    PlayButton.gameObject.SetActive(true);
    //    FireOnPostMainMenuOpened();
    //}

    //void OnBlurClosed()
    //{
    //    CameraEffects.OnBlurClosed -= OnBlurClosed;
    //    UIFadeTween.gameObject.SetActive(false);
    //    FireOnPostMainMenuClosed();
    //}
}
