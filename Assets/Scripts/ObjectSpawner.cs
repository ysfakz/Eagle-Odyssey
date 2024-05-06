using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
    public static ObjectSpawner Instance{ get; private set; }
    public GameObject gemPrefab;
    public GameObject obstaclePrefab;
    public GameObject powerUpPrefab;
    public float gemSpawnProbability = 0.5f;
    public float powerUpSpawnProbability = 0.04f;
    public float spawnInterval = 2f;
    public float minY = 30f;
    public float maxY = 50f;
    public float moveSpeed = 15f;
    public float destroyDelay = 5f;
    private bool isActive = false;
    private Coroutine spawnRoutine;
    private Vector3 spawnPosition = new Vector3(30f, 0f, -3.7f);
    private Quaternion spawnRotation = Quaternion.Euler(-90f, 0f, 0f);

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        //Checks the current game state and if there is a spawner active in order to start spawning.
        if (GameManager.Instance.IsGamePlaying() && !isActive) {
            spawnRoutine = StartCoroutine(SpawnObjects());
            isActive = true;
        }
        //Checks the game state and if there is a spawner active in order to stop spawning.
        if (GameManager.Instance.IsGameOver() && isActive) {
            StopCoroutine(spawnRoutine);
            isActive = false;
        }
    }

    /*Function that spawns the gem, obstacle, and power-up objects.*/
    private IEnumerator SpawnObjects() {
        while (true) {
            // Randomly choose which object to spawn based on probabilities
            GameObject prefabToSpawn = Random.Range(0f, 1f) < gemSpawnProbability ? gemPrefab : obstaclePrefab;
            if (Random.Range(0f, 1f) < powerUpSpawnProbability){
                prefabToSpawn = powerUpPrefab;
            }

            // Calculate the random Y position within the specified range
            float randomY = Random.Range(minY, maxY);

            // Spawn the chosen object at the specified position and rotation
            Vector3 spawnPositionWithOffset = spawnPosition + new Vector3(0f, randomY, 0f);
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPositionWithOffset, spawnRotation);

            // Move the spawned object forward
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.velocity = Vector3.left * moveSpeed;
            }

            // Destroy the spawned object after a delay if it's not destroyed by collision
            Destroy(spawnedObject, destroyDelay);

            // Wait for the spawn interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public bool IsActiveCheck() {
        return isActive = true;
    }
}
