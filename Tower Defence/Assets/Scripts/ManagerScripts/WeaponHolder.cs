using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponPrefabs; // Array to store weapon prefabs
    [SerializeField] private Transform playerTransform; // Reference to the player transform
    [SerializeField] private CombatManager combatManager; // Reference to the CombatManager
    [SerializeField] private EnemyManager enemyManager; // Reference to the EnemyManager

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    public void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponPrefabs.Length)
        {
            Debug.LogError("Invalid weapon index.");
            return;
        }

        // Instantiate the selected weapon prefab and make it a child of the player
        GameObject weaponInstance = Instantiate(weaponPrefabs[weaponIndex], playerTransform);
        Weapon weaponScript = weaponInstance.GetComponent<Weapon>();

        // Notify the EnemyManager that the weapon has been selected
        if (enemyManager != null)
        {
            enemyManager.SetWeaponSelected(true);
        }
    }

    public void CloseWeaponSelectionMenu()
    {
        // Close the weapon selection UI
        GameObject.Find("WeaponSelectionMenu").SetActive(false);
    }
}
