using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 30.0f;

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime), Space.Self);
    }
}