using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points
    [SerializeField] private List<GameObject> enemyPrefabs; // Enemy prefabs to spawn

    [Header("Wave System")]
    [SerializeField] private int totalEnemies = 4; // Base number of enemies per wave

    private List<GameObject> activeEnemies = new List<GameObject>(); // Track active enemies

    private void Update()
    {
        // Check if all enemies are killed to start the next wave
        activeEnemies.RemoveAll(enemy => enemy == null); // Remove null references (destroyed enemies)
        if (activeEnemies.Count == 0)
        {
            StartWave();
        }
    }

    private void StartWave()
    {
        int waveNumber = WaveManager.Instance.GetWaveNumber();
        Debug.Log($"Starting Wave: {waveNumber}");

        for (int i = 0; i < totalEnemies; i++)
        {
            SpawnEnemy(enemyPrefabs[0]); // Assuming index 0 is the basic enemy
        }

        totalEnemies += 2; // Increase enemies per wave for scaling difficulty
        WaveManager.Instance.IncrementWave(); // Notify WaveManager to increment the wave
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Select a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Set a fixed z position (e.g., z = 1)
        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.z = 1f;

        // Instantiate the enemy prefab at the chosen spawn point
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, spawnPoint.rotation);

        // Add the spawned enemy to the active enemies list (if applicable)
        activeEnemies.Add(spawnedEnemy);
    }
}
