using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform lookAt;

    void Start()
    {
        try
        {
            lookAt = GameObject.Find("Player").transform;
        }
        catch
        {
            lookAt.position = Vector3.zero;
        }
    }

    void Update()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        transform.forward = (lookAt.position - transform.position).normalized;
    }
}
