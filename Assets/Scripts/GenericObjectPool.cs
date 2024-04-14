using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Abstract Object Pool Class. Generate Pool and Provide access to the current pool member. 
/// </summary>
public class GenericObjectPool
{
    private GameObject _gameObject;
    private int _poolSize = 5;
    private ObjectPool<GameObject> _pool;
    
    public GenericObjectPool(int maxAmount, GameObject prefab)
    {
        _gameObject = prefab;
        _poolSize = maxAmount;
        _pool = new ObjectPool<GameObject>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, _poolSize, _poolSize);
    }
    
    private GameObject CreateObject()
    {
        GameObject instance = GameObject.Instantiate(_gameObject, _gameObject.transform.position, _gameObject.transform.rotation);
        instance.GetComponent<GenericObjectPoolClient>().ObjectPool = _pool;
        return instance;
    }

    private void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }
    
    private void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
    
    private void OnDestroyPooledObject(GameObject obj)
    {
        Object.Destroy(obj);
    }
    
    public GameObject Get()
    {
        return _pool.Get();
    }
}
