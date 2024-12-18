using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private int initialSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count == 0)
        {
            InitializePool();
        }

        GameObject obj = pool.Dequeue();
        if (obj == null)
        {
            obj = Instantiate(prefab);
        }
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
