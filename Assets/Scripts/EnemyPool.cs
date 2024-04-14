using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [Tooltip("Max amount of enemies int the pool for each type")]
    [SerializeField] private int _poolSize = 5;
    [SerializeField] private Enemy _dogPrefab;
    [SerializeField] private Enemy _kangarooPrefab;
    [SerializeField] private Enemy _birdPrefab;
    
    private ObjectPool<Enemy> _dogPool;
    private ObjectPool<Enemy> _kanagarooPool;
    private ObjectPool<Enemy> _birdPool;
    
    private int _dogCount;
    private int _kangarooCount;
    private int _birdCount;

    public void Initialize()
    {
        _dogCount = 0;
        _dogPool = new ObjectPool<Enemy>(CreateDog, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, _poolSize, _poolSize);
        _kanagarooPool = new ObjectPool<Enemy>(CreateKangaroo, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, _poolSize, _poolSize);
        _birdPool = new ObjectPool<Enemy>(CreateBird, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, _poolSize, _poolSize);
    }

    private Enemy CreateDog()
    {
        Enemy enemyInstance = Instantiate(_dogPrefab);
        enemyInstance.ObjectPool = _dogPool;
        enemyInstance.name = "Dog" + _dogCount;
        _dogCount++;
        return enemyInstance;
    }
    
    private Enemy CreateKangaroo()
    {
        Enemy enemyInstance = Instantiate(_kangarooPrefab);
        enemyInstance.ObjectPool = _kanagarooPool;
        enemyInstance.name = "Kangaroo" + _kangarooCount;
        _kangarooCount++;
        return enemyInstance;
    }
    
    private Enemy CreateBird()
    {
        Enemy enemyInstance = Instantiate(_birdPrefab);
        enemyInstance.ObjectPool = _birdPool;
        enemyInstance.name = "Bird" + _birdCount;
        _birdCount++;
        return enemyInstance;
    }
    
    private void OnGetFromPool(Enemy obj)
    {
        obj.gameObject.SetActive(true);
    }
    
    private void OnReleaseToPool(Enemy obj)
    {
        obj.gameObject.SetActive(false);
    }
    
    private void OnDestroyPooledObject(Enemy obj)
    {
        Destroy(obj.gameObject);
    }

    public Enemy Get(EnemyTypes enemyType)
    {
        switch (enemyType)
        {
            case EnemyTypes.Dog:
                if (_dogPool.CountAll <= _poolSize)
                {
                    return _dogPool.Get();
                }
                break;
            case EnemyTypes.Kangaroo:
                if (_kanagarooPool.CountAll <= _poolSize)
                {
                    return _kanagarooPool.Get();
                }
                break;
            case EnemyTypes.Bird:
                if (_birdPool.CountAll <= _poolSize)
                {
                    return _birdPool.Get();
                }
                break; 
        }
        return null;
    }
}
