using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour, IAttack
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 7;
    [SerializeField] private float lifespan = 3f; // Duration before the bullet is returned to the pool
    private float lifeTimer;
    private float maxDistance;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 direction;
    private bool isReturning;

    private AttackComponent attackComponent;
    private ObjectPool bulletPool;

    private void Start()
    {
        attackComponent = GetComponent<AttackComponent>();
    }

    private void OnEnable()
    {
        lifeTimer = lifespan;
        isReturning = false;
        startPosition = transform.position;
    }

    // made public so AttackComponent able to get the damage value from the bullet
    public int GetAttackDamage()
    {
        return damage;
    }

    public void SetTarget(Vector3 target, float maxDistance)
    {
        this.maxDistance = maxDistance;
        targetPosition = target;
        direction = (targetPosition - startPosition).normalized;
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer > 0f)
        {
            if (!isReturning)
            {
                transform.position += direction * speed * Time.deltaTime;
                if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
                {
                    isReturning = true;
                    direction = -direction;
                }
            }
            else
            {
                transform.position += direction * speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, startPosition) <= 0.1f)
                {
                    if (bulletPool != null)
                    {
                        bulletPool.ReturnObject(this.gameObject);
                    }
                }
            }
        }
        else
        {
            if (bulletPool != null)
            {
                bulletPool.ReturnObject(this.gameObject);
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
