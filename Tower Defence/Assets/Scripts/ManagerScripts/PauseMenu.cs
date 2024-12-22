using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Reference to the pause menu UI
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // Scene name for the main menu

    private void Start()
    {
        // Ensure the pause menu UI is hidden at the start
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    private void Update()
    {
        // Listen for pause input (e.g., "Escape" key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameManager.gameManagerInstance != null)
        {
            GameManager.gameManagerInstance.TogglePause();
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(GameManager.gameManagerInstance.IsPaused());
            }
        }
        else
        {
            Debug.LogError("GameManager instance not found. Ensure GameManager is properly initialized.");
        }
    }

    public void ResumeGame()
    {
        // Resume the game and hide the pause menu
        if (GameManager.gameManagerInstance != null)
        {
            GameManager.gameManagerInstance.TogglePause();
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(false);
            }
        }
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        Debug.Log("Returning to the main menu...");
        if (GameManager.gameManagerInstance != null)
        {
            GameManager.gameManagerInstance.TogglePause(); // Ensure the game is unpaused
        }
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
