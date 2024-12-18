using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// shoots 3 bullets at once. Has the shortest range but highest damage
public class MultiShoot : Weapon
{
    private float angle = 20f;
    public override void ShootBullet()
    {
        Vector3 firePoint = transform.position + firePointOffset;

        // Central bullet
        Shoot(firePoint, GetMouseWorldPosition());

        // Left bullet
        Vector3 leftDirection = Quaternion.Euler(0, 0, angle) * (GetMouseWorldPosition() - firePoint).normalized;
        Shoot(firePoint, firePoint + leftDirection * 10f); // Arbitrary distance

        // Right bullet
        Vector3 rightDirection = Quaternion.Euler(0, 0, -angle) * (GetMouseWorldPosition() - firePoint).normalized;
        Shoot(firePoint, firePoint + rightDirection * 10f); // Arbitrary distance
    }

    public void Shoot(Vector3 firePoint, Vector3 targetPosition)
    {
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = firePoint;
        bullet.transform.rotation = Quaternion.identity;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(targetPosition);
            bulletScript.SetPoolReference(bulletPool);
        }
    }
}
