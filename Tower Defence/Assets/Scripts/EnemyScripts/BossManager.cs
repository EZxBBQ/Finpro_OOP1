using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab; // Boss prefab to spawn
    [SerializeField] private Transform spawnPoint; // Boss spawn point

    [Header("Basic Enemy Settings")]
    [SerializeField] private GameObject[] basicEnemyPrefabs; // Array of basic enemy prefabs
    [SerializeField] private Transform[] enemySpawnPoints; // Spawn points for basic enemies
    [SerializeField] private float spawnInterval = 5f; // Time interval for spawning basic enemies

    [Header("Scene Settings")]
    [SerializeField] private string mainSceneName = "MainMenu"; // Name of the main battle scene

    [SerializeField] private GameObject transitionImage;
    [SerializeField] private Animator animator;

    [SerializeField] private Text youWInText;

    private GameObject bossInstance;
    private bool isBossAlive = true; // Tracks if the boss is alive

    private void Start()
    {
        SpawnBoss();
        StartCoroutine(EnemySpawnerRoutine());
        transitionImage.SetActive(false);
        youWInText.gameObject.SetActive(false);
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

    private IEnumerator EnemySpawnerRoutine()
    {
        while (isBossAlive)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnBasicEnemies();
        }
    }

    private void SpawnBasicEnemies()
    {
        if (basicEnemyPrefabs.Length == 0 || enemySpawnPoints.Length == 0)
        {
            Debug.LogError("BasicEnemyPrefabs or EnemySpawnPoints not assigned.");
            return;
        }

        for (int i = 0; i < 2; i++) // Spawn two basic enemies
        {
            // Select a random enemy prefab
            GameObject randomEnemyPrefab = basicEnemyPrefabs[Random.Range(0, basicEnemyPrefabs.Length)];

            // Select a random spawn point
            Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];

            // Instantiate the enemy
            Instantiate(randomEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        Debug.Log("Basic enemies spawned by the boss.");
    }

    public void OnBossDefeated()
    {
        isBossAlive = false; // Stop the spawning routine
        youWInText.gameObject.SetActive(true);
        LoadScene(mainSceneName);
    }

     public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;    // make sure the game is unpaused when loading new scene
        transitionImage.SetActive(true);    
        StartCoroutine(TransitionAnimation(sceneName));
    }

    private IEnumerator TransitionAnimation(string sceneName)
    {
        animator.SetTrigger("isStarted");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
