using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnLocation;
    private float timeInterval = 2.0f;
    private int numberOfCopies = 10;

    private int spawnCount = 0;

    void Start()
    {
        InvokeRepeating("SpawnPrefab", 0.0f, timeInterval);
    }

    void SpawnPrefab()
    {
        if (spawnCount < numberOfCopies)
        {
            GameObject newFish = Instantiate(prefabToSpawn, spawnLocation.position, Quaternion.identity);
            float randomWeight = Random.Range(0f, 3f);

            Fish fishComponent = newFish.GetComponent<Fish>();
            if (fishComponent != null)
            {
                fishComponent.setWeight(randomWeight);
            }
            spawnCount++;
        }
        else
        {
            // Stop the spawning once the desired number of copies is reached
            CancelInvoke("SpawnPrefab");
        }
    }
}