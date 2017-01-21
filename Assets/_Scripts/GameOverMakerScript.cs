using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMakerScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
            GameManager.Instance.GameOver();
    }
}
