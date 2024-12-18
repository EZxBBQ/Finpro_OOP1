using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameManager;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            DestroyPlayerAndGameManager();
        }
    }

    private void DestroyPlayerAndGameManager()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");

        if (player != null)
        {
            Destroy(player);
            player = null; // set to null manually so GameReset doesnt display missing reference in editor
        }

        if (gameManager != null)
        {
            Destroy(gameManager);
            gameManager = null;
        }
    }
}
