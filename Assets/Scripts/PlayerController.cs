using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    private enum MovementMode { Kinematic, Rigidbody }

    [Header("Movement Mode")]
    [SerializeField] private MovementMode movementMode = MovementMode.Kinematic;

    [Header("Movement Settings")]
    [SerializeField] private float kinematicNormalSpeed = 10.0f;
    [SerializeField] private float kinematicTurboSpeed = 20.0f;
    [SerializeField] private float physicsNormalSpeed = 15.0f;
    [SerializeField] private float physicsTurboSpeed = 30.0f;
    [SerializeField] private float maxLinearSpeed = 20.0f;
    [SerializeField] private float linearDamping = 1.5f;
    private float horizontal;
    private float vertical;
    private float movementY;
    private Rigidbody rb;
    private bool isTurboActive;

    [Header("Rotation Settings")]
    [SerializeField] private float kinematicMouseSensitivity = 375.0f;
    [SerializeField] private float kinematicRollSpeed = 75.0f;
    [SerializeField] private float physicsMouseSensitivity = 40.0f;
    [SerializeField] private float physicsRollSpeed = 8.0f;
    [SerializeField] private float maxAngularSpeed = 10.0f;
    [SerializeField] private float angularDamping = 5.0f;
    private float mouseX;
    private float mouseY;
    private float rollInput;

    [Header("Shoot Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform look;
    [SerializeField] private Transform bulletsContainer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb.linearDamping = linearDamping;
        rb.angularDamping = angularDamping;

        ApplyMovementMode();
    }

    private void Update()
    {
        ReadInput();

        if (movementMode == MovementMode.Kinematic)
        {
            KinematicMovement();
            KinematicRotation();
        }
    }

    private void FixedUpdate()
    {
        if (movementMode == MovementMode.Rigidbody)
        {
            PhysicsMovement();
            PhysicsRotation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CelestialBody") || other.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void ApplyMovementMode()
    {
        ResetPhysics();

        rb.isKinematic = movementMode == MovementMode.Kinematic;
    }

    private void ResetPhysics()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void ReadInput()
    {
        // CHANGE MOVEMENT MODE
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMovementMode();
        }

        // TURBO
        isTurboActive = Input.GetKey(KeyCode.LeftShift);

        // MOVEMENT
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movementY = 0.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            movementY += 1.0f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            movementY -= 1.0f;
        }

        // ROTATION
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rollInput = 0.0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rollInput += 1.0f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            rollInput -= 1.0f;
        }

        // SHOOT
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(shootSound);
            Instantiate(bullet, look.position, transform.rotation, bulletsContainer);
        }

        // EXIT GAME
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void ToggleMovementMode()
    {
        if (movementMode == MovementMode.Kinematic)
        {
            movementMode = MovementMode.Rigidbody;
        }
        else
        {
            movementMode = MovementMode.Kinematic;
        }

        ApplyMovementMode();
    }

    private void KinematicMovement()
    {
        float currentSpeed = GetCurrentSpeed(kinematicNormalSpeed, kinematicTurboSpeed);
        Vector3 movement = new Vector3(horizontal, movementY, vertical).normalized;

        transform.Translate(movement * (currentSpeed * Time.deltaTime), Space.Self);
    }

    private void KinematicRotation()
    {
        float pitch = mouseY * kinematicMouseSensitivity * Time.deltaTime;
        float yaw = mouseX * kinematicMouseSensitivity * Time.deltaTime;
        float roll = rollInput * kinematicRollSpeed * Time.deltaTime;

        transform.Rotate(-pitch, yaw, roll, Space.Self);
    }

    private void PhysicsMovement()
    {
        float currentSpeed = GetCurrentSpeed(physicsNormalSpeed, physicsTurboSpeed);
        Vector3 localMovement = new Vector3(horizontal, movementY, vertical).normalized;
        Vector3 worldMovement = transform.TransformDirection(localMovement);

        rb.AddForce(worldMovement * currentSpeed, ForceMode.Acceleration);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxLinearSpeed);
    }

    private void PhysicsRotation()
    {
        float pitch = mouseY * physicsMouseSensitivity;
        float yaw = mouseX * physicsMouseSensitivity;
        float roll = rollInput * physicsRollSpeed;
        Vector3 localTorque = new Vector3(-pitch, yaw, roll);
        Vector3 worldTorque = transform.TransformDirection(localTorque);

        if (rb.angularVelocity.magnitude < maxAngularSpeed)
        {
            rb.AddTorque(worldTorque, ForceMode.Acceleration);
        }

        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxAngularSpeed);
    }

    private float GetCurrentSpeed(float normal, float turbo)
    {
        if (isTurboActive)
        {
            return turbo;
        }

        return normal;
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}