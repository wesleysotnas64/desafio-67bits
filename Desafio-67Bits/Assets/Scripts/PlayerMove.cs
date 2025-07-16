using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float baseSpeed;
    [SerializeField]
    private float currentSpeed;

    private PlayerAnimation playerAnimation;
    public StackController stackcController;
    public FixedJoystick fixedJoystick;

    void Start()
    {
        currentSpeed = baseSpeed;
        direction = Vector3.zero;

        playerAnimation = GetComponent<PlayerAnimation>();
        fixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
    }

    void Update()
    {
        ControllersKeyboard();
        ControllersJoystick();
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

    private void ControllersJoystick()
    {
        direction = Vector3.zero;

        direction.z = fixedJoystick.Direction.y;
        direction.x = fixedJoystick.Direction.x;

        direction.Normalize();

        Move(direction);
    }

    public void Move(Vector3 _direction)
    {
        if (playerAnimation.attack) return;

        bool isMoving = !(_direction == Vector3.zero);

        if (isMoving) playerAnimation.SetMove();
        else
        {
            playerAnimation.SetIdle();
            return;
        }

        transform.forward = _direction.normalized;
        transform.position += currentSpeed * Time.deltaTime * transform.forward;
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Enemy":
                Vector3 posEnemy = collision.gameObject.transform.position;
                posEnemy.y = transform.position.y;
                transform.forward = (posEnemy - transform.position).normalized;
                StartCoroutine(playerAnimation.WaitAttack());
                StartCoroutine(collision.gameObject.GetComponent<Enemy>().Struck(0.45f, transform.forward));

                break;

            case "Carcass":
                stackcController.AddElementAtStack(collision.gameObject);
                break;

            default:
                break;
        }
    
    }
}
