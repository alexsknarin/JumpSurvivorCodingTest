using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;



public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDog;
    private float _spawnFrequency = 2f;
    private float _prevTime;
    private ObjectPool _enemyDogPool;

    private float GetRandomDirection()
    {
        if (Random.Range(-1f, 1f) > 0)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _prevTime = Time.time;
        _enemyDogPool = new ObjectPool(5, _enemyDog);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _prevTime > _spawnFrequency)
        {
            //var currentEnemy = Instantiate(_enemyDog, _enemyDog.transform.position, _enemyDog.transform.rotation);
            GameObject currentEnemy = _enemyDogPool.GetPooledObject();
            if (currentEnemy != null)
            {
                float dir = GetRandomDirection();
                currentEnemy.GetComponent<Enemy>().SpawnSetup(dir);
                _prevTime = Time.time;                
            }
        }
    }
}
