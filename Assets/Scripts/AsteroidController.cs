using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2.5f;
    [SerializeField] private float moveSpeed = 2.0f;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Rotation();
        Movement();
    }

    private void Rotation()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Movement()
    {
        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
    }
}
