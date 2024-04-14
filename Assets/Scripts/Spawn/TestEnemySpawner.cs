using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    private float _localTime;
    private float _waitDuration = 3f;


    private void SpawnEnemy(EnemyTypes enemyType)
    {
        var enemy = _enemyPool.Get(enemyType);
        if (enemy != null)
        {
            enemy.SetupSpawn(1f);
        }
    }

    private void Awake()
    {
        _enemyPool.Initialize();
    }

    private void Start()
    {
        _localTime = 0;
    }

    private void Update()
    {
        float waitPhase = _localTime / _waitDuration;
        if (waitPhase > 1)
        {
            SpawnEnemy(EnemyTypes.Dog);
            _localTime = 0;
        }
        _localTime += Time.deltaTime;
    }


    // private void SpawnEnemy(EnemyPool enemyPool, ref float enemySpawnPrevTime, float spawnRate, 
    //     float enemyGlobalDir, bool useEnemyGlobal, bool useStateGlobal, ref bool isFirstSpawnInState)
    // {
    //     if ((Time.time - enemySpawnPrevTime > spawnRate) || isFirstSpawnInState)
    //     {
    //         GameObject currentEnemy = enemyPool.GetPooledObject();
    //         if (currentEnemy != null)
    //         {
    //             // Define a direction
    //             _currentEnemyDir = GetRandomDirection();
    //             if (useEnemyGlobal)
    //             {
    //                 _currentEnemyDir = enemyGlobalDir;
    //             }
    //             if (useStateGlobal)
    //             {
    //                 _currentEnemyDir = _globalStateDir;
    //             }
    //             currentEnemy.GetComponent<Enemy>().SetupSpawn(_currentEnemyDir);
    //         }
    //         enemySpawnPrevTime = Time.time;
    //         isFirstSpawnInState = false;
    //     }
    // }
}
