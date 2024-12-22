using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private int health = 1000;         // Boss health
    [SerializeField] private int attackDamage = 10;    // Damage dealt to the player on collision

    private Transform player;                          // Reference to the player's transform
    private bool isNearWall = false;                   // To check if the boss is near the invisible wall

    private void Start()
    {
        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene! Make sure the Player GameObject has the tag 'Player'.");
        }
    }

    private void Update()
    {
        // No movement logic here to make the boss static
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // When the boss collides with the player, deal damage to the player
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(attackDamage); // Reduce player's health
                Debug.Log("Player hit! Dealt " + attackDamage + " damage.");
            }
        }
        else if (collision.CompareTag("PlayerBullet")) // Check if the boss is hit by a bullet
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.GetAttackDamage());
                Destroy(collision.gameObject); // Destroy the bullet upon hitting the boss
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce boss health
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss has been defeated!");
        Destroy(gameObject); // Destroy the boss GameObject
    }
}
