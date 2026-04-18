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
        Destroy(gameObject);

        if (other.gameObject.layer == LayerMask.NameToLayer("CelestialBody"))
        {
            Debug.Log("Bullet collided with celestial body!");
        }
    }
}