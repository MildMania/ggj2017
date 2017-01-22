using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static Action OnPostMainMenuClosed, OnPreMainMenuClosed, OnPostMainMenuOpened, OnPreMainMenuOpened;

    public MMTweenAlpha UIFadeTween;
    public GameObject MainMenuUI, TitleObj;
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

        PlayButton.onClick.AddListener(OnPlayButtonPressed);

        MainMenuUI.gameObject.SetActive(true);

        StartListeningEvents();
    }

    void OnDestroy()
    {
        StopListeningEvents();
    }

    void StartListeningEvents()
    {
        GameManager.OnGameInitialize += OpenMainMenu;
        GameManager.OnPreGameStart += CloseMainMenu;
        GameManager.OnGameClosed += OpenMainMenu;
    }

    void StopListeningEvents()
    {
        GameManager.OnGameInitialize -= OpenMainMenu;
        GameManager.OnPreGameStart -= CloseMainMenu;
        GameManager.OnGameClosed -= OpenMainMenu;
    }

    void OnPlayButtonPressed()
    {
        SceneManager.Instance.PlayPressed();
    }

    void OpenMainMenu()
    {
        FireOnPreMainMenuOpened();

        MainMenuUI.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(true);
        TitleObj.gameObject.SetActive(true);

        FireOnPostMainMenuOpened();
        //CameraEffects.OnBlurOpened += OnBlurOpened;
        //CameraEffects.Instance.OpenBlur();

        UIFadeTween.PlayForward();
    }

    void CloseMainMenu()
    {
        FireOnPreMainMenuClosed();

        //UIFadeTween.gameObject.SetActive(false);
        FireOnPostMainMenuClosed();

        PlayButton.gameObject.SetActive(false);
        TitleObj.gameObject.SetActive(false);

        if (MainMenuUI.gameObject.activeSelf)
            UIFadeTween.AddOnFinish(() => MainMenuUI.gameObject.SetActive(false), false).PlayReverse();
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
