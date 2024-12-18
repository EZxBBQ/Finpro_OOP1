using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager used to manage the game state
// Apply singleton pattern
public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance { get; set; }

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

    // properly clean up the singleton instance when the object is destroyed
    private void OnDestroy()
    {
        Debug.Log("GameManager destroyed.");
        if (gameManagerInstance == this)
        {
            gameManagerInstance = null;
        }
    }
}
