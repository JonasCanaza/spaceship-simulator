using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10.0f;

    [Header("Rotation Settings")]
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float rollSpeed = 50.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        float movementY = 0.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            movementY = 1.0f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            movementY = -1.0f;
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), movementY, Input.GetAxis("Vertical"));
        transform.Translate(movement * (speed * Time.deltaTime));
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float roll = 0.0f;

        if (Input.GetKey(KeyCode.Q))
        {
            roll = 1.0f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            roll = -1.0f;
        }

        roll *= Time.deltaTime * rollSpeed;
        transform.Rotate(-mouseY, mouseX, roll, Space.Self);
    }
}