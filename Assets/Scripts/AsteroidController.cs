using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class AsteroidController : MonoBehaviour
{
    [Header("Speed Setting")]
    [SerializeField] private float rotationSpeed = 3.5f;
    [SerializeField] private float moveSpeed = 3.5f;

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