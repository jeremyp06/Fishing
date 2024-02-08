using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnLocation;
    private float initialTimeInterval = 3.0f;
    private int spawnCount = 0;
    private float spawnIntervalDecreaseRate = 0.05f;

    void Start()
    {
        // Start spawning fish
        StartSpawning();
    }

    void StartSpawning()
    {
        // Start spawning with initial time interval
        InvokeRepeating("SpawnPrefab", 0.0f, initialTimeInterval);
    }

    void SpawnPrefab()
    {
        // Instantiate fish object using factory method
        CreateFish();

        // Increase spawn count
        spawnCount++;

        // Decrease spawn interval logarithmically
        float newTimeInterval = initialTimeInterval / Mathf.Log(1.3f * spawnCount + 1);

        // Ensure the new time interval does not become too small
        newTimeInterval = Mathf.Max(newTimeInterval, 0.1f);

        // Cancel the current spawning schedule
        CancelInvoke("SpawnPrefab");

        // Start spawning again with updated time interval
        InvokeRepeating("SpawnPrefab", newTimeInterval, newTimeInterval);
    }

    void CreateFish()
    {
        // Instantiate fish object using factory method
        GameObject newFish = FishFactory.CreateFish(prefabToSpawn, spawnLocation.position);
        if (newFish != null)
        {
            // Set random weight for the fish
            Fish fishComponent = newFish.GetComponent<Fish>();
            if (fishComponent != null)
            {
                float randomWeight = Random.Range(0f, 3f);
                fishComponent.setWeight(randomWeight);
            }
        }
    }
}

public static class FishFactory
{
    public static GameObject CreateFish(GameObject prefab, Vector3 spawnPosition)
    {
        // Generate a random y-coordinate within the range of -2 to 2
        float randomY = Random.Range(-2f, 2f);

        // Set the spawn position with the random y-coordinate
        Vector3 position = new Vector3(spawnPosition.x, randomY, spawnPosition.z);

        // Instantiate fish object and return it
        return UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
    }
}
