using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEndurance
{
    public static Player playerInstance { get; private set; }
    private PlayerLevel playerLevel;
    private EnduranceComponent enduranceComponent;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int defense = 10;
    [SerializeField] private int currentHealth;

    public delegate void HealthChanged(int currentHealth, int maxHealth);
    public event HealthChanged OnHealthChanged;

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

        enduranceComponent = GetComponentInChildren<EnduranceComponent>();
        if (enduranceComponent == null)
        {
            Debug.LogError("EnduranceComponent not found in child object.");

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        playerLevel = GetComponentInChildren<PlayerLevel>();
        if (playerLevel == null)
        {
            Debug.LogError("PlayerLevel component not found in child object.");

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health at the start
    }

    private void OnDestroy()
    {
        if (playerInstance == this)
        {
            playerInstance = null;
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetDefense()
    {
        return defense;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        OnHealthChanged?.Invoke(currentHealth, maxHealth); // Notify listeners
    }

    public void IncreaseDefense(int amount)
    {
        defense += amount;
    }

    public void RestoreHealthToMax()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth); // Notify listeners
    }

    public void TakeDamage(int damage)
    {
        int effectiveDamage = Mathf.Max(damage - defense, 0); // Consider defense
        currentHealth = Mathf.Max(currentHealth - effectiveDamage, 0); // Prevent health from going below 0

        OnHealthChanged?.Invoke(currentHealth, maxHealth); // Notify listeners

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Implement player death logic here
    }

    public void LevelUp()
    {
        if (playerLevel != null)
        {
            playerLevel.LevelUp(this);
        }
    }
}
