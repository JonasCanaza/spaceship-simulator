using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform asteroidsContainer;
    [SerializeField] private float spawnInterval = 7.5f;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0.0f;
        }
    }

    private void SpawnAsteroid()
    {
        if (spawnPoints.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomPoint = spawnPoints[randomIndex];

        Instantiate(asteroidPrefab, randomPoint.position, Quaternion.identity, asteroidsContainer);
    }
}