using UnityEngine;
using DG.Tweening;

public class CameraShakeScript : MonoBehaviour
{
    public static CameraShakeScript Instance { get; private set; }

    Transform _cameraTransform;

    void Awake()
    {
        Instance = this;

        _cameraTransform = Camera.main.transform;

        StartListeningEvents();
    }

    void StartListeningEvents()
    {
        GameManager.OnGameOver += OnGameOver;
    }

    void StopListeningEvents()
    {
        GameManager.OnGameOver -= OnGameOver;
    }

    public void ShakeCamera()
    {
        _cameraTransform.DOShakeRotation(0.2f, 1f, 100);
    }

    void OnGameOver()
    {
        ShakeCamera();
    }
}
