using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Vector3 firePointOffset;
    protected ObjectPool bulletPool;


    protected virtual void Start()
    {
        bulletPool = GetComponent<ObjectPool>();
    }

    public abstract void ShootBullet();

    protected Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
