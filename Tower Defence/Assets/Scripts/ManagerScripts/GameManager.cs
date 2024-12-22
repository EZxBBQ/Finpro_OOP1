using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance { get; private set; }
    private bool isPaused = false;

    private void Awake()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("GameManager destroyed.");
        if (gameManagerInstance == this)
        {
            gameManagerInstance = null;
        }
    }

    // Toggle pause state
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        Debug.Log("Game " + (isPaused ? "paused." : "resumed."));
    }

    // Check if the game is paused
    public bool IsPaused()
    {
        return isPaused;
    }
}
