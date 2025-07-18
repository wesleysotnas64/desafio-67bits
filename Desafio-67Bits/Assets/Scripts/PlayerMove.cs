using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float baseSpeed;
    [SerializeField]
    private float currentSpeed;
    public bool clubIsActive;
    public GameObject clubObject;

    private PlayerAnimation playerAnimation;
    public StackController stackcController;
    public FixedJoystick fixedJoystick;

    void Start()
    {
        currentSpeed = baseSpeed;
        direction = Vector3.zero;
        // clubIsActive = false;

        playerAnimation = GetComponent<PlayerAnimation>();
        try
        {
            fixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        }
        catch {}
    }

    void Update()
    {
        ControllersKeyboard();
        ControllersJoystick();

        clubObject.SetActive(clubIsActive);
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
        if (!fixedJoystick) return;
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

    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Enemy":
                Vector3 posEnemy = other.gameObject.transform.position;
                posEnemy.y = transform.position.y;
                transform.forward = (posEnemy - transform.position).normalized;
                StartCoroutine(playerAnimation.WaitAttack());
                StartCoroutine(other.gameObject.GetComponent<Enemy>().Struck(0.45f, transform.forward, clubIsActive ? 10.0f : 5.0f));

                break;

            case "Carcass":
                if(stackcController.NotMaximumLoad())
                    other.GetComponent<StackElement>().PlayAudioTaken();
                stackcController.AddElementAtStack(other.gameObject);
                break;

            case "Coin":
                other.GetComponent<CoinStartSpawn>().EnableDisable();
                other.GetComponent<AudioSource>().Play();
                break;

            case "GrateTriggerArea":
                stackcController.RemoveAllElements();
                break;

            case "StoreTriggerArea":
                GameObject.Find("Canvas").GetComponent<SceneController>().OpenStore();
                break;

            default:
                break;
        }
    }
}
