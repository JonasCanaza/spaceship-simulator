using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    [SerializeField] private float timeLife = 1.0f;

    private void Update()
    {
        timeLife -= Time.deltaTime;

        if (timeLife <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}