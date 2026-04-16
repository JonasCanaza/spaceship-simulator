using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private void Update()
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
}