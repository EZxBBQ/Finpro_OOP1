using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IAttack
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifespan = 1f; // Duration before the bullet is returned to the pool
    private float lifeTimer;

    private AttackComponent attackComponent;
    private Vector3 targetPosition;
    private ObjectPool bulletPool;


    private void Start()
    {
        attackComponent = GetComponent<AttackComponent>();
    }

    private void OnEnable()
    {
        lifeTimer = lifespan;
    }

    // made public so AttackComponent able to get the damage value from the bullet
    public int GetAttackDamage()
    {
        return damage;
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        Vector3 direction = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
           lifeTimer -= 2f * Time.deltaTime;
            Debug.Log(lifeTimer);
            if (lifeTimer > 0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                if (bulletPool != null)
                {
                    bulletPool.ReturnObject(this.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackComponent != null)
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                return;
            }
            else
            {
                attackComponent.DealDamage(collision.gameObject);
                bulletPool.ReturnObject(this.gameObject);
            }  
        }
    }

    public void SetPoolReference(ObjectPool pool)
    {
        bulletPool = pool;
    }
}
