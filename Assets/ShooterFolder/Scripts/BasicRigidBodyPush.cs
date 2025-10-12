using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
    [Range(0.5f, 5f)] public float strength = 1.1f;
    public bool canPush = true;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!canPush) return;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;
        if (hit.moveDirection.y < -0.3f) return; // не толкаем вниз

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
        body.AddForce(pushDir * strength, ForceMode.Impulse);
    }
}