using UnityEngine;

public class LookAtShipScript : MonoBehaviour
{
    public Transform TargetLookAt;

    void Start()
    {
        TargetLookAt = ShipController.Instance.gameObject.transform;
    }
    void FixedUpdate()
    {
        if (TargetLookAt == null)
            return;

        transform.LookAt(TargetLookAt);

        Vector3 angles = transform.localEulerAngles;
        angles.x = 0f;

        transform.localEulerAngles = angles;
    }
}
