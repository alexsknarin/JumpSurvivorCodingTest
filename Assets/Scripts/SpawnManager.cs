using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDog;
    [SerializeField] private float _dogSwawnRate = 2f;
    private float _prevDogTime;
    private ObjectPool _enemyDogPool;

    [SerializeField] private GameObject _enemyKangaroo;
    [SerializeField] private float _kangarooSwawnRate = 5f;
    private float _prevKangarooTime;
    private float _kangarooDelay = 10f;
    private float _kangarooDelayTime;
    private ObjectPool _enemyKangarooPool;

    [SerializeField] private GameObject _enemyBird;
    [SerializeField] private float _birdSwawnRate = 15f;
    private float _prevBirdTime;
    private float _birdDelay = 20f;
    private float _birdDelayTime;
    private ObjectPool _enemyBirdPool;
    
    [SerializeField] private FloatVariable _gameTime;
    
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

    public void InitSpawn()
    {
        _prevDogTime = _gameTime.Value;
        _enemyDogPool = new ObjectPool(5, _enemyDog);

        _kangarooDelayTime = _gameTime.Value;
        _prevKangarooTime = _kangarooDelayTime + _kangarooDelay;
        _enemyKangarooPool = new ObjectPool(3, _enemyKangaroo);
        
        _birdDelayTime = Time.time;
        _prevBirdTime = _kangarooDelayTime + _kangarooDelay;
        _enemyBirdPool = new ObjectPool(3, _enemyBird);
    }

    // Update is called once per frame
    void Update()
    {
        // Dog
        SpawnEnemy(0f, 0f, ref _prevDogTime, _dogSwawnRate, _enemyDogPool);

        //Kangaroo
        SpawnEnemy(_kangarooDelayTime, _kangarooDelay, ref _prevKangarooTime, _kangarooSwawnRate, _enemyKangarooPool);
        
        // Bird
        SpawnEnemy(_birdDelayTime, _birdDelay, ref _prevBirdTime, _birdSwawnRate, _enemyBirdPool);
    }

    private void SpawnEnemy(float delayTime, float delay, ref float prevEnemyTime, float enemySpawnRate, ObjectPool enemyPool)
    {
        if (_gameTime.Value - delayTime > delay)
        {

            if (_gameTime.Value - prevEnemyTime > enemySpawnRate)
            {
                GameObject currentEnemy =  enemyPool.GetPooledObject();
                if (currentEnemy != null)
                {
                    float dir = GetRandomDirection();
                    currentEnemy.GetComponent<Enemy>().SpawnSetup(dir);
                    prevEnemyTime = _gameTime.Value;                
                }
            }
        }
    }
}
