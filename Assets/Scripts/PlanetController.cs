using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float orbitSpeed = 4.0f;
    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField] private float speed = 2.0f;

    [Header("Transform Settings")]
    [SerializeField] private Transform center;
    [SerializeField] private bool isDestructible = false;

    private void Update()
    {
        if (center != null)
        {
            transform.RotateAround(center.position, Vector3.up, -orbitSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime), Space.Self);
        }

        transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDestructible)
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}