using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// EnduranceComponent used to manage the health and defense of the object its attached to
public class EnduranceComponent : MonoBehaviour, IAttackable
{
    private int maxHealth;
    private int currentHealth;
    private int defense;

    // events to be invoked when health is changed or when the object dies
    public UnityEvent onHealthChanged;
    public UnityEvent onDeath;

    private IEndurance enduranceInterface;

    private void Awake()
    {
        enduranceInterface = GetComponentInParent<IEndurance>();
        if (enduranceInterface != null)
        {
            maxHealth = enduranceInterface.GetMaxHealth();
            defense = enduranceInterface.GetDefense();
        }
        else
        {
            Debug.LogError("IEndurance is not found in parent object.");

            // quit the editor if the interface is not found
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        maxHealth = enduranceInterface.GetMaxHealth();
        defense = enduranceInterface.GetDefense();
    }

    public void RestoreHealthToMax()
    {
        currentHealth = maxHealth;
        onHealthChanged.Invoke();
    }

    // need to be public so AttackComponent can access it
    public void TakeDamage(int damage)
    {   int damageDealt = damage - defense;
        damageDealt = Mathf.Clamp(damageDealt, 1, damage);
        currentHealth -= damageDealt;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onHealthChanged.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDeath.Invoke();
        // Add additional death logic here (e.g., disable the object, play animation, etc.)
    }
}
