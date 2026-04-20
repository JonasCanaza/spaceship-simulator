using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class BulletController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speed = 50.0f;
    [SerializeField] private GameObject hitSound;
    private Transform hitsSoundsContainer;

    private void Start()
    {
        hitsSoundsContainer = GameObject.Find("Hit Sound Container").transform;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CelestialBody"))
        {
            Destroy(gameObject);
            Explosion();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Explosion();
        }
    }

    private void Explosion()
    {
        Instantiate(hitSound, transform.position, Quaternion.identity, hitsSoundsContainer);
    }
}