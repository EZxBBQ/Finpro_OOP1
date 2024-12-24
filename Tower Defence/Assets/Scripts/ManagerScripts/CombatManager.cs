using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CombatManager manage the flow of combat between player and enemies
public class CombatManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private float shootCooldown = 0.2f;
    private float nextShootTime;
    private Weapon currentWeapon;
    private Animator weaponAnimator;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    private void Update() // shooting need to be responsive so its using Update() instead of FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Mouse0 pressed");
            weaponAnimator?.SetBool("isShooting", true);
            Shoot();
        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextShootTime) // Assuming "Fire1" is configured in Input settings
        {
            Shoot();
        }
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            weaponAnimator?.SetBool("isShooting", false);
        }
    }

    // made public so WeaponSelectionMenu can access it
    public void FindEquippedWeapon()
    {
        // used to find the weapon attached to the player as a child object
        currentWeapon = playerTransform.GetComponentInChildren<Weapon>();
        if (currentWeapon == null)
        {
            Debug.LogError("Weapon not found in child object.");
        }
        else
        {
            weaponAnimator = currentWeapon.GetComponent<Animator>();
        }
    }

    // method to make current weapon shoot
    private void Shoot()
    {
        currentWeapon?.ShootBullet();
        nextShootTime = Time.time + shootCooldown;
    }

    // Future methods for handling player and enemy interactions can be added here
}
