using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Enemies:")]
    [SerializeField] private GameObject _enemyDog;
    [SerializeField] private int _maximumDogs;
    [SerializeField] private GameObject _enemyKangaroo;
    [SerializeField] private int _maximumKangaroos;
    [SerializeField] private GameObject _enemyBird;
    [SerializeField] private int _maximumBirds;

    [Header("Spawn States")] 
    [SerializeField] private List<SpawnState> _spawnStates;
    private SpawnState _currentSpawnState;
    private int _currentSpawnStateIndex = 0;


    private ObjectPool _enemyDogPool;
    private ObjectPool _enemyKangarooPool;
    private ObjectPool _enemyBirdPool;

    private float _globalSpawnPrevTime;
    private float _dogSpawnPrevTime;
    private float _kangarooSpawnPrevTime;
    private float _birdSpawnPrevTime;
    
    private bool _delayMode = false;
    
    private float GetRandomDirection()
    {
        if (UnityEngine.Random.Range(-1f, 1f) > 0)
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
        _enemyDogPool = new ObjectPool(_maximumDogs, _enemyDog);
        _enemyKangarooPool = new ObjectPool(_maximumKangaroos, _enemyKangaroo);
        _enemyBirdPool = new ObjectPool(_maximumBirds, _enemyBird);

        ChangeSpawnState();
    }

    private void ChangeSpawnState()
    {
        _globalSpawnPrevTime = Time.time;
        if (_currentSpawnStateIndex == 3)
        {
            Debug.Break();
        }
        _currentSpawnState = _spawnStates[_currentSpawnStateIndex];
        _delayMode = _currentSpawnState.UseStateDelay;
    }
    
    private void Start()
    {
        InitSpawn();
    }

    private void Update()
    {
        // State Delay
        if (_delayMode)
        {
            if (Time.time - _globalSpawnPrevTime > _currentSpawnState.StateDelay)
            {
                _delayMode = false;
                _globalSpawnPrevTime = Time.time;
                _dogSpawnPrevTime = Time.time;
                _kangarooSpawnPrevTime = Time.time;
                _birdSpawnPrevTime = Time.time;
            }
            else
            {
                return;
            }
        }
        
        // Spawn Enemies
        if (Time.time - _globalSpawnPrevTime < _currentSpawnState.StateDuration)
        {
           // Spawn Dogs
           if (_currentSpawnState.DogsEnabled)
           {
               SpawnEnemy(_enemyDogPool, ref _dogSpawnPrevTime, _currentSpawnState.DogSpawnRate);
           }
           
           // Spawn Kangaroos
           if (_currentSpawnState.KangaroosEnabled)
           {
               SpawnEnemy(_enemyKangarooPool, ref _kangarooSpawnPrevTime, _currentSpawnState.KangarooSpawnRate);
           }
           
           // Spawn Birds
           if (_currentSpawnState.BirdsEnabled)
           {
               SpawnEnemy(_enemyBirdPool, ref _birdSpawnPrevTime, _currentSpawnState.BirdSpawnRate);
           }
           
        }
        else
        {
            if (_currentSpawnStateIndex < _spawnStates.Count-1)
            {
                _currentSpawnStateIndex++;
                ChangeSpawnState();
            }
        }
        
    }

    private void SpawnEnemy(ObjectPool enemyPool, ref float enemySpawnPrevTime, float spawnRate)
    {
        if (Time.time - enemySpawnPrevTime > spawnRate)
        {
            GameObject currentEnemy = enemyPool.GetPooledObject();
            if (currentEnemy != null)
            {
                float dir = GetRandomDirection();
                currentEnemy.GetComponent<Enemy>().SpawnSetup(dir);
            }
            enemySpawnPrevTime = Time.time;
        }
    }
}
