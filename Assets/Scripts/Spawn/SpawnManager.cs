using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour, IPausable
{
    [Header("Enemies:")]
    [SerializeField] private GameObject _enemyDog;
    [SerializeField] private int _maximumDogs;
    [SerializeField] private GameObject _enemyKangaroo;
    [SerializeField] private int _maximumKangaroos;
    [SerializeField] private GameObject _enemyBird;
    [SerializeField] private int _maximumBirds;

    [Header("Spawn States")] 
    private SpawnStateCollection _spawnCollection;

    [Header("Difficulty Levels:")]
    [SerializeField] private IntVariable _dificultyLevel;
    [SerializeField] private List<SpawnStateCollection> _spawnCollectionDifficulties;
    
    private SpawnState _currentSpawnState;
    private int _currentSpawnStateIndex = 0;
    private int _prevSpawnStateIndex;
    private bool _isLearningPhase = true;

    private ObjectPool _enemyDogPool;
    private ObjectPool _enemyKangarooPool;
    private ObjectPool _enemyBirdPool;

    private float _globalSpawnPrevTime;
    private float _dogSpawnPrevTime;
    private float _kangarooSpawnPrevTime;
    private float _birdSpawnPrevTime;

    private bool _isDogDelayed;
    private bool _isKangarooDelayed;
    private bool _isBirdDelayed;
    
    private bool _isDogFirstSpawnInState;
    private bool _isKangarooFirstSpawnInState;
    private bool _isBirdFirstSpawnInState;
    
    private float _globalStateDir;
    private float _dogGlobalStateDir;
    private float _kangarooGlobalStateDir;
    private float _birdGlobalStateDir;
    private float _currentEnemyDir;
    
    private bool _delayMode = false;
    
    private bool _isPaused = false;
    
    // State Debug UI
    public delegate void SpawnStateChanged(string StateName);
    public static event SpawnStateChanged OnSpawnStateChanged;

    private void Start()
    {
        Game.Pausables.Add(this);
    }

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
        
        // Select Difficulty Level
        _spawnCollection = _spawnCollectionDifficulties[_dificultyLevel.Value];
        ChangeSpawnState();
    }

    private void ChangeSpawnState()
    {
        _globalSpawnPrevTime = Time.time;
        _globalStateDir = GetRandomDirection();
        _dogGlobalStateDir = GetRandomDirection();
        _kangarooGlobalStateDir = GetRandomDirection();
        _birdGlobalStateDir = GetRandomDirection();
        
        if (_isLearningPhase)
        {
            _currentSpawnState = _spawnCollection.SpawnStatesLearn[_currentSpawnStateIndex];    
        }
        else
        {
            _prevSpawnStateIndex = _currentSpawnStateIndex; 
            _currentSpawnStateIndex = (int)Random.Range(0, _spawnCollection.SpawnStatesMainLoop.Count);
            if (_currentSpawnStateIndex == _prevSpawnStateIndex)
            {
                _currentSpawnStateIndex = (int)Random.Range(0, _spawnCollection.SpawnStatesMainLoop.Count);
            }
            _currentSpawnState = _spawnCollection.SpawnStatesMainLoop[_currentSpawnStateIndex];
        }
        
        OnSpawnStateChanged(_currentSpawnState.name);
        _delayMode = _currentSpawnState.UseStateDelay;
        _isDogDelayed = true;
        if (Mathf.Approximately(_currentSpawnState.DogSpawnDelay, 0f))
        {
            _isDogDelayed = false;
        }
        _isKangarooDelayed = true;
        if (Mathf.Approximately(_currentSpawnState.KangarooSpawnDelay, 0f))
        {
            _isKangarooDelayed = false;
        }
        _isBirdDelayed = true;
        if (Mathf.Approximately(_currentSpawnState.BirdSpawnDelay, 0f))
        {
            _isBirdDelayed = false;
        }
        
        _isDogFirstSpawnInState = true;
        _isKangarooFirstSpawnInState = true;
        _isBirdFirstSpawnInState = true;
    }
    
    private void Update()
    {
        if (_isPaused)
        {
            return;
        }
        
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
           if (_currentSpawnState.DogsEnabled)
           {
               if (_isDogDelayed)
               {
                   if (Time.time - _currentSpawnState.DogSpawnDelay > _globalSpawnPrevTime)
                   {
                       _isDogDelayed = false;
                       _dogSpawnPrevTime = Time.time;
                   }
               }
               else
               {
                   SpawnEnemy(_enemyDogPool, ref _dogSpawnPrevTime, _currentSpawnState.DogSpawnRate, _dogGlobalStateDir, 
                       _currentSpawnState.DogsRandomOncePerState, _currentSpawnState.DogsUseGlobalStateDirection,
                       ref _isDogFirstSpawnInState);
               }
           }
           if (_currentSpawnState.KangaroosEnabled)
           {
               if (_isKangarooDelayed)
               {
                   if (Time.time - _currentSpawnState.KangarooSpawnDelay > _globalSpawnPrevTime)
                   {
                       _isKangarooDelayed = false;
                       _kangarooSpawnPrevTime = Time.time;
                   }
               }
               else
               {
                   SpawnEnemy(_enemyKangarooPool, ref _kangarooSpawnPrevTime, _currentSpawnState.KangarooSpawnRate, _kangarooGlobalStateDir, 
                       _currentSpawnState.KangaroosRandomOncePerState, _currentSpawnState.KangaroosUseGlobalStateDirection,
                       ref _isKangarooFirstSpawnInState);
               }
           }
           if (_currentSpawnState.BirdsEnabled)
           {
               if (_isBirdDelayed)
               {
                   if (Time.time - _currentSpawnState.BirdSpawnDelay > _globalSpawnPrevTime)
                   {
                       _isBirdDelayed = false;
                       _birdSpawnPrevTime = Time.time;
                   }
               }
               else
               {
                   SpawnEnemy(_enemyBirdPool, ref _birdSpawnPrevTime, _currentSpawnState.BirdSpawnRate, _birdGlobalStateDir,
                       _currentSpawnState.BirdsRandomOncePerState, _currentSpawnState.BirdsUseGlobalStateDirection,
                       ref _isBirdFirstSpawnInState);
               }
           }
           
        }
        else
        {
            // Switch SpawnState
            if (_currentSpawnStateIndex < _spawnCollection.SpawnStatesLearn.Count-1)
            {
                _currentSpawnStateIndex++;
                ChangeSpawnState();
            }
            else
            {
                if (_spawnCollection.SpawnStatesMainLoop.Count > 0)
                {
                    _isLearningPhase = false;
                    ChangeSpawnState();                    
                }
                else
                {
                    return;
                }
            }
        }
        
    }

    private void SpawnEnemy(ObjectPool enemyPool, ref float enemySpawnPrevTime, float spawnRate, 
        float enemyGlobalDir, bool useEnemyGlobal, bool useStateGlobal, ref bool isFirstSpawnInState)
    {
        if ((Time.time - enemySpawnPrevTime > spawnRate) || isFirstSpawnInState)
        {
            GameObject currentEnemy = enemyPool.GetPooledObject();
            if (currentEnemy != null)
            {
                // Define a direction
                _currentEnemyDir = GetRandomDirection();
                if (useEnemyGlobal)
                {
                    _currentEnemyDir = enemyGlobalDir;
                }
                if (useStateGlobal)
                {
                    _currentEnemyDir = _globalStateDir;
                }
                currentEnemy.GetComponent<Enemy>().SpawnSetup(_currentEnemyDir);
            }
            enemySpawnPrevTime = Time.time;
            isFirstSpawnInState = false;
        }
    }

    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }
}
