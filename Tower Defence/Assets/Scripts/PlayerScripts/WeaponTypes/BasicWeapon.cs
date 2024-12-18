using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// starter weapon, shoot regular bullets
public class BasicWeapon : Weapon
{
    public override void ShootBullet()
    {
        Vector3 firePoint = transform.position + firePointOffset;
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = firePoint;
        bullet.transform.rotation = Quaternion.identity;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(GetMouseWorldPosition());
            bulletScript.SetPoolReference(bulletPool);
        }
    }
}
