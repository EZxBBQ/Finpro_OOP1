using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab; // Boss prefab to spawn
    [SerializeField] private Transform spawnPoint; // Boss spawn point
    [SerializeField] private string mainSceneName = "MainBattle"; // Name of the main battle scene

    private GameObject bossInstance;

    private void Start()
    {
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null || spawnPoint == null)
        {
            Debug.LogError("BossPrefab or SpawnPoint not assigned.");
            return;
        }

        // Spawn the boss
        bossInstance = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Boss spawned.");
    }

    public void OnBossDefeated()
    {
        Debug.Log("Boss defeated! Returning to the main battle scene...");
        SceneManager.LoadScene(mainSceneName); // Return to the main battle scene
    }
}
