using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetScript : MonoBehaviour
{
    public Transform TargetLookAt;

    private void Awake()
    {
        if (TargetLookAt == null)
            TargetLookAt = Camera.main.gameObject.transform;
    }
    void FixedUpdate()
    {
        if (TargetLookAt == null)
            return;

        transform.LookAt(TargetLookAt);
    }
}
