using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    void LateUpdate()
    {
        Transform cam = Camera.main.transform;

        transform.LookAt(transform.position + cam.forward);
    }
}
