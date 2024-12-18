using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// shoot a bullet that can return to the player. deal more damage than basic weapon if it hits the target twice
public class Boomerang : Weapon
{
    private float maxDistance = 7f;

    public override void ShootBullet()
    {
        Vector3 firePoint = transform.position + firePointOffset;
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = firePoint;
        bullet.transform.rotation = Quaternion.identity;

        BoomerangBullet bulletScript = bullet.GetComponent<BoomerangBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(GetMouseWorldPosition(), maxDistance);
            bulletScript.SetPoolReference(bulletPool);
        }
    }
}
