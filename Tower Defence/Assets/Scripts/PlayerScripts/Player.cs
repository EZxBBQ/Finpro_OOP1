using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player used to manage the player object
// Apply singleton pattern
public class Player : MonoBehaviour, IEndurance
{
    public static Player playerInstance { get; set; } // need to be public for consistent access
    private PlayerLevel playerLevel;
    private EnduranceComponent enduranceComponent;
    private int maxHealth = 100;
    private int defense = 10;


    private void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // get the EnduranceComponent from the child object
        enduranceComponent = GetComponentInChildren<EnduranceComponent>();
        if (enduranceComponent == null)
        {
            Debug.LogError("EnduranceComponent not found in child object.");

            // quit the editor if the component is not found
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        // get the PlayerLevel component from the child object
        playerLevel = GetComponentInChildren<PlayerLevel>();
        if (enduranceComponent == null)
        {
            Debug.LogError("EnduranceComponent not found in child object.");

            // quit the editor if the component is not found
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

    // properly clean up the singleton instance when the object is destroyed
    private void OnDestroy()
    {
        Debug.Log("Player destroyed.");
        if (playerInstance == this)
        {
            playerInstance = null;
        }
    }

    // need to be public so EnduranceComponent can access it
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    // need to be public so EnduranceComponent can access it
    public int GetDefense()
    {
        return defense;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
    }

    public void IncreaseDefense(int amount)
    {
        defense += amount;
    }

    public void RestoreHealthToMax()
    {
        enduranceComponent.RestoreHealthToMax();
    }

    public void LevelUp()
    {
        playerLevel.LevelUp(this);
    }
}
