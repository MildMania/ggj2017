using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMakerScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            GameManager.Instance.GameOver();
    }
}
