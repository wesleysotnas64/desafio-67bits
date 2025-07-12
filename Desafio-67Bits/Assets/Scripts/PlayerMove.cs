using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float baseSpeed;
    [SerializeField]
    private float currentSpeed;

    void Start()
    {
        currentSpeed = baseSpeed;
        direction = Vector3.zero;
    }

    void Update()
    {
        ControllersKeyboard();
    }

    private void ControllersKeyboard()
    {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) direction.z += 1.0f;
        if (Input.GetKey(KeyCode.S)) direction.z -= 1.0f;
        if (Input.GetKey(KeyCode.A)) direction.x -= 1.0f;
        if (Input.GetKey(KeyCode.D)) direction.x += 1.0f;

        direction.Normalize();

        Move(direction);
    }

    public void Move(Vector3 _direction)
    {
        if (_direction == Vector3.zero) return;

        transform.forward = _direction.normalized;
        transform.position += currentSpeed * Time.deltaTime * transform.forward;
    }
}
