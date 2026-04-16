using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10.0f;

    [Header("Rotation Settings")]
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float rollSpeed = 50.0f;

    [Header("Shoot Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform look;
    [SerializeField] private Transform bulletsContainer;

    private float horizontal;
    private float vertical;
    private float movementY;

    private float mouseX;
    private float mouseY;
    private float rollInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ReadInput();
        Movement();
        Rotation();
    }

    private void ReadInput()
    {
        // MOVEMENT
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movementY = 0.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            movementY = 1.0f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            movementY = -1.0f;
        }

        // ROTATION
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rollInput = 0.0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rollInput = 1.0f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            rollInput = -1.0f;
        }

        // SHOOT
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, look.position, transform.rotation, bulletsContainer);
        }
    }

    private void Movement()
    {
        Vector3 movement = new Vector3(horizontal, movementY, vertical).normalized;
        transform.Translate(movement * (speed * Time.deltaTime), Space.Self);
    }

    private void Rotation()
    {
        float pitch = mouseY * mouseSensitivity * Time.deltaTime;
        float yaw = mouseX * mouseSensitivity * Time.deltaTime;
        float roll = rollInput * rollSpeed * Time.deltaTime;

        transform.Rotate(-pitch, yaw, roll, Space.Self);
    }
}