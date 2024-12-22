using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 3f;         // Movement speed of the enemy
    [SerializeField] private int health = 50;         // Enemy's health
    [SerializeField] private int attackDamage = 10;   // Damage dealt to the player on collision

    private Transform player;                         // Reference to the player's transform
    private bool isNearWall = false;                  // To check if the enemy is near the invisible wall

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
        // Follow the player unless the enemy is near the invisible wall
        if (player != null && !isNearWall)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InvisibleWall"))
        {
            // Stop the enemy from moving closer to the player
            isNearWall = true;
        }
        else if (collision.CompareTag("Player"))
        {
            // When the enemy collides with the player, deal damage to the player
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(attackDamage); // Reduce player's health
                Debug.Log("Player hit! Dealt " + attackDamage + " damage.");

            }
        }
        else if (collision.CompareTag("PlayerBullet")) // Check if the enemy is hit by a bullet
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.GetAttackDamage());
                Destroy(collision.gameObject); // Destroy the bullet upon hitting the enemy
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        // Allow the enemy to move again if it leaves the wall area
        if (collision.CompareTag("InvisibleWall"))
        {
            isNearWall = false;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce enemy health
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died.");
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
