using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float normalSpeed = 10.0f;
    [SerializeField] private float turboSpeed = 20.0f;
    private float horizontal;
    private float vertical;
    private float movementY;
    private bool isTurboActive;

    [Header("Rotation Settings")]
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float rollSpeed = 50.0f;
    private float mouseX;
    private float mouseY;
    private float rollInput;

    [Header("Shoot Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform look;
    [SerializeField] private Transform bulletsContainer;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CelestialBody"))
        {
            Debug.Log("Player collided with celestial body!");
        }
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

        // TURBO
        isTurboActive = Input.GetKey(KeyCode.LeftShift);

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
        float currentSpeed;

        if (isTurboActive)
        {
            currentSpeed = turboSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        Vector3 movement = new Vector3(horizontal, movementY, vertical).normalized;
        transform.Translate(movement * (currentSpeed * Time.deltaTime), Space.Self);
    }

    private void Rotation()
    {
        float pitch = mouseY * mouseSensitivity * Time.deltaTime;
        float yaw = mouseX * mouseSensitivity * Time.deltaTime;
        float roll = rollInput * rollSpeed * Time.deltaTime;

        transform.Rotate(-pitch, yaw, roll, Space.Self);
    }
}