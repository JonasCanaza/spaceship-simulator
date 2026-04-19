using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class BulletController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speed = 50.0f;

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CelestialBody"))
        {
            Destroy(gameObject);
            Debug.Log("Bullet collided with celestial body!");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("Bullet collided with asteroid");
        }
    }
}