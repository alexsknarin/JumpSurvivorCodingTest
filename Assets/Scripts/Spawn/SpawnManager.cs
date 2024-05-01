/* This is basically the place where all game progression is being controlled.
 * Because game should generate enemies endlessly there is a task to make spawned enemy combinations
 * diverse and somewhat controllable. Therefore instead of relying on fully random way of spawning enemies
 * SpawnManager works with "spawnCollections" - Scriptable objects containing lists of smaller spawn behaviour bits.
 * PROS:
 *  - Interesting spawning patterns could be authored manually instead of relying on random.
 *  - Game designer has control over order of spawn bits, which is especially important for the tutorial phase.
 *  - Designer can replace spawnCollections at any times  which makes it convenient to experiment or test isolated
 * parts of the experience.
 *
 * All spawning logic is implemented in SpawnManager, spawnStates dont contain any logic - only variables.
 * It practically turns SpawnManager into a sort of Data driven StateMachine, or a very simple interpreter for a visually
 * Constructed script.
 *
 * There are three inputs for spawnCollections - which one to use is decided based on difficulty level.
 *
 * Spawn Manager holds Object Pools for each kind of enemy, it initializes them and controls their usage.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Controls generation of enemies for the whole game. 
/// </summary>
public class SpawnManager : MonoBehaviour, IPausable
{
    // TODO: separate pooled enemies and non pooled ones
    [Header("Enemies:")]
    [SerializeField] private EnemyPool _enemyPool;

    [Header("Healer:")]
    [SerializeField] private GameObject _healBird;

    [SerializeField] private float _minHealTime;
    [SerializeField] private float _maxHealTime;

    [Header("Car:")] 
    [SerializeField] private EnemyCar _carEnemy;

    [Header("Spawn States")] 
    private SpawnStateCollection _spawnCollection;
    
    [Header("Difficulty Levels:")]
    [SerializeField] private IntVariable _dificultyLevel;
    [SerializeField] private List<SpawnStateCollection> _spawnCollectionDifficulties;
    
    private bool _isSpawnEnabled = false;
    
    private SpawnState _currentSpawnState;
    [SerializeField] private int _currentSpawnStateIndex = 0;
    private int _prevSpawnStateIndex;
    private bool _isLearningPhase = true;

    private float _globalSpawnPrevTime;
  
    // Enemy timers
    private float _dogSpawnLocalTime;
    private float _kangarooSpawnLocalTime;
    private float _birdSpawnLocalTime;

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
    
    // Healing Bird
    private GameObject _healBirdInstance;
    private Medkit _medkit;
    private bool _isHealNeeded = false;
    private bool _isFirstBirdSpawned = false;
    private float _timeToNextHeal;
    private float _prevHealTime;
    private float _healLocalTime;
    
    // Car 
    private float _carLocalTime;
    private float _carSpawnRate = 15f;
    
    // State Debug UI
    public static event Action<string> SpawnStateChanged;

    private void OnEnable()
    {
        PlayerHealth.NearDeathStarted += NearDeathHandle;
        PlayerHealth.NearDeathEnded += NearDeathEndedHandle;
        TutorialManager.FirstBirdSpawned += FirstBirdSpawnedHandle;
    }

    private void OnDisable()
    {
        PlayerHealth.NearDeathStarted -= NearDeathHandle;
        PlayerHealth.NearDeathEnded -= NearDeathEndedHandle;
        TutorialManager.FirstBirdSpawned -= FirstBirdSpawnedHandle;
    }
    
    public void InitSpawn()
    {       
        _enemyPool.Initialize();
        Game.Pausables.Add(this);
        
        // Select Difficulty Level
        _spawnCollection = _spawnCollectionDifficulties[_dificultyLevel.Value];
        
        // Spawn and setup Heal Bird TODO: consider saving it in the scene same as a car
        _healBirdInstance = GameObject.Instantiate(_healBird, _healBird.transform.position, _healBird.transform.rotation);
        _healBirdInstance.SetActive(false);
        _medkit = _healBirdInstance.GetComponent<Medkit>();
        _isSpawnEnabled = true;
        
        _dogSpawnLocalTime = 0;
        _kangarooSpawnLocalTime = 0;
        _birdSpawnLocalTime = 0;
        
        
        // Spawn And Setup a Car
        _carLocalTime = 0;
        
        ChangeSpawnState();
    }

    private void Update()
    {
        if (_isPaused)
        {
            return;
        }

        if (!_isSpawnEnabled)
        {
            return;
        }
        
        // Car Spawn

        if (_currentSpawnStateIndex > 8)
        {
            if (_carLocalTime > _carSpawnRate)
            {
                SpawnCar();
                _carLocalTime = 0f;
                _carSpawnRate = Random.Range(10f, 18f);
            }
            else
            {
                _carLocalTime += Time.deltaTime;
            }
        }
        
        
        // State Delay
        if (_delayMode)
        {
            if (Time.time - _globalSpawnPrevTime > _currentSpawnState.StateDelay)
            {
                _delayMode = false;
                // _globalSpawnLocalTime = 0;
                _globalSpawnPrevTime = Time.time;
                _dogSpawnLocalTime = 0;
                _kangarooSpawnLocalTime = 0;
                _birdSpawnLocalTime = 0;
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
                       _dogSpawnLocalTime = 0;
                   }
               }
               else
               {
                   TrySpawnEnemy(
                       EnemyTypes.Dog, 
                       ref _dogSpawnLocalTime, 
                       _currentSpawnState.DogSpawnRate, 
                       _dogGlobalStateDir, 
                       _currentSpawnState.DogsRandomOncePerState, 
                       _currentSpawnState.DogsUseGlobalStateDirection,
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
                       _kangarooSpawnLocalTime = 0;
                   }
               }
               else
               {
                   TrySpawnEnemy(
                       EnemyTypes.Kangaroo, 
                       ref _kangarooSpawnLocalTime, 
                       _currentSpawnState.KangarooSpawnRate, 
                       _kangarooGlobalStateDir, 
                       _currentSpawnState.KangaroosRandomOncePerState, 
                       _currentSpawnState.KangaroosUseGlobalStateDirection,
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
                       _birdSpawnLocalTime = 0;
                   }
               }
               else
               {
                   TrySpawnEnemy(
                       EnemyTypes.Bird, 
                       ref _birdSpawnLocalTime, 
                       _currentSpawnState.BirdSpawnRate, 
                       _birdGlobalStateDir,
                       _currentSpawnState.BirdsRandomOncePerState, 
                       _currentSpawnState.BirdsUseGlobalStateDirection,
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

        CheckForHealBird();
    }
    
    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }
    
    /// <summary>
    /// Spawn Enemy from the Object Pool
    /// </summary>
    /// <param name="enemyType">Current EnemyType.</param>
    /// <param name="enemySpawnLocalTime">Reference to the local Spawn Time for the current enemy.</param>
    /// <param name="spawnRate">HOw much time you need to wait before spawning a next enemy of current type for the current spawnState.</param>
    /// <param name="enemyGlobalDir">Single movement direction for the whole duration of current spawnState.</param>
    /// <param name="useEnemyGlobal">Use single direction for the spawnState instead a random one for each spawn.</param>
    /// <param name="useStateGlobal">Use single state direction that will be shared with other enemies.</param>
    /// <param name="isFirstSpawnInState">Required to make the first enemy in state to be spawned without delay.</param>
    private void TrySpawnEnemy(EnemyTypes enemyType, ref float enemySpawnLocalTime, float spawnRate, 
        float enemyGlobalDir, bool useEnemyGlobal, bool useStateGlobal, ref bool isFirstSpawnInState)
    {
        if ((enemySpawnLocalTime > spawnRate) || isFirstSpawnInState)
        {
            Enemy currentEnemy = _enemyPool.Get(enemyType);
            Game.Pausables.Add(currentEnemy);
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
                currentEnemy.SetupSpawn(_currentEnemyDir);
            }
            enemySpawnLocalTime = 0;
            isFirstSpawnInState = false;
        }
        enemySpawnLocalTime += Time.deltaTime;
    }
    
    private void SpawnCar()
    {
        _carEnemy.SetupSpawn(GetRandomDirection());
    }
    
    private int GetRandomDirection()
    {
        return Random.Range(0, 2) * 2 - 1;
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
        
        // TODO: remove this event from release version of the code.
        SpawnStateChanged?.Invoke(_currentSpawnState.name);
        
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
    
    private void CheckForHealBird()
    {
        if (_isHealNeeded && _isFirstBirdSpawned)
        {
            if (_healLocalTime > _timeToNextHeal)
            {
                StartHealBird();
                HealSetup();
            }
        }
        _healLocalTime += Time.deltaTime;
    }
    
    private void StartHealBird()
    {
        _healBirdInstance.GetComponent<Enemy>().SetupSpawn(GetRandomDirection());
        _medkit.Init();
    }
    
    private void HealSetup()
    {
        _isHealNeeded = true;
        _timeToNextHeal = Random.Range(_minHealTime, _maxHealTime);
        _healLocalTime = 0;
    }

    private void NearDeathHandle()
    {
        HealSetup();
    }

    private void NearDeathEndedHandle()
    {
        _isHealNeeded = false;
    }

    private void FirstBirdSpawnedHandle()
    {
        _isFirstBirdSpawned = true;
        CheckForHealBird();
    }
}
