using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [SerializeField] private string bossSceneName = "BossBattle"; // Name of the boss battle scene
    [SerializeField] private string mainSceneName = "MainBattle"; // Name of the main battle scene
    private int waveNumber = 1; // Current wave number
    private bool isBossBattleActive = false; // Tracks if the boss battle is active

    private void Awake()
    {
        // Ensure only one instance of WaveManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementWave()
    {
        waveNumber++;
        Debug.Log($"Wave {waveNumber} started.");

        // Check if the wave is a multiple of 5
        if (waveNumber % 5 == 0)
        {
            StartBossBattle();
        }
        else
        {
            // Logic for the next wave can be added here if needed
        }
    }

    private void StartBossBattle()
    {
        Debug.Log("Starting boss battle! Transitioning to the boss scene.");
        isBossBattleActive = true; // Set the boss battle as active
        SceneManager.LoadScene(bossSceneName); // Load the boss battle scene
    }

    public void ReturnToMainBattle()
    {
        Debug.Log("Boss defeated! Returning to the main battle scene.");
        isBossBattleActive = false; // Reset the boss battle state
        SceneManager.LoadScene(mainSceneName); // Load the main battle scene
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public bool IsBossBattleActive()
    {
        return isBossBattleActive;
    }
}
