using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float orbitSpeed = 4.0f;
    [SerializeField] private float rotationSpeed = 4.0f;

    [Header("Transform Settings")]
    [SerializeField] private Transform center;

    private void Update()
    {
        transform.RotateAround(center.position, Vector3.up, -orbitSpeed * Time.deltaTime);
        transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
    }
}