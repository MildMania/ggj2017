using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        Utilities.Initialize();
    }

    public void PlayPressed()
    {
        GameManager.Instance.StartGame();
    }
}
