using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerLevel used to manage the player leveling system
public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private List<Sprite> levelSprites = new List<Sprite>();
    private SpriteRenderer spriteRenderer;
    private int healthIncreasePerLevel = 100;
    private int defenseIncreasePerLevel = 10;
    private int currentLevel = 1;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the player object.");
        }
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    // made public so Player can access it
    public void LevelUp(Player player)
    {
        currentLevel++;
        player.IncreaseMaxHealth(healthIncreasePerLevel);
        player.IncreaseDefense(defenseIncreasePerLevel);
        player.RestoreHealthToMax();

        if (currentLevel < levelSprites.Count)
        {
            spriteRenderer.sprite = levelSprites[currentLevel - 1];
        }
    }
}
