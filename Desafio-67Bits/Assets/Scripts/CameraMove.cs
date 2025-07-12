using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    public float speed;
    void Start()
    {
        try
        {
            target = GameObject.Find("Player").transform;
        }
        catch
        {
            target.position = Vector3.zero;
        }
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 newPosition = ((target.position + offset) - transform.position).magnitude > 0.01f
            ? Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime)
            : (target.position + offset);

        transform.position = newPosition;
    }
}
