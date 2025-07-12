using UnityEngine;

public class StackElement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed;
    public Vector3 newPosition;

    void Update()
    {
        if (target != null)
        {
            newPosition = ((target.position + offset) - transform.position).magnitude > 0.01f
            ? Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime)
            : (target.position + offset);

            transform.position = newPosition;
        }
    }
}
