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
    [SerializeField] private bool _testingMode = false;
    [SerializeField] private int _startTestIndex;
    
    // TODO: separate pooled enemies and non pooled ones
    [Header("Pooled Enemies:")]
    [SerializeField] private EnemyPool _enemyPool;

    [Header("Single Enemies:")]
    [Header("Healer Bird:")]
    [SerializeField] private Enemy _healBird;

    [SerializeField] private float _minHealTime;
    [SerializeField] private float _maxHealTime;

    [Header("Car:")] 
    [SerializeField] private EnemyCar _carEnemy;

    [Header("Spawn States")] 
    private SpawnStateCollection _spawnCollection;
    
    [Header("Difficulty Levels:")]
    [SerializeField] private IntVariable _dificultyLevel;
    [SerializeField] private List<SpawnStateCollection> _spawnCollectionDifficulties;
    
    [SerializeField] private SpawnLevels _currentSpawnLevel;
    [SerializeField] private int _currentSpawnStateIndex = 0;
    private bool _isSpawnEnabled = false;
    private SpawnState _currentSpawnState;
    
    private int _prevSpawnStateRandomIndex;
    private int _currentSpawnStateRandomIndex = 0;
    
    private float _globalSpawnLocalTime;
 
    // Enemy Timers
    private float _dogSpawnLocalTime;
    private float _kangarooSpawnLocalTime;
    private float _birdSpawnLocalTime;

    private bool _isDogDelayed;
    private bool _isKangarooDelayed;
    private bool _isBirdDelayed;
    
    // First Spawn in State
    private bool _isDogFirstSpawnInState;
    private bool _isKangarooFirstSpawnInState;
    private bool _isBirdFirstSpawnInState;
    
    // Directions
    private float _globalStateDir;
    private float _dogGlobalStateDir;
    private float _kangarooGlobalStateDir;
    private float _birdGlobalStateDir;
    private float _currentEnemyDir;
    
    private bool _isDelayedSpawnState = false;
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
    private bool _isCarSpawnPaused = false;
    private float _carSpawnPauseDuration = 2.5f;
    private float _carSpawnPauseLocalTime = 0.0f;
    
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

        // Spawn and setup Heal Bird
        _healBirdInstance = _healBird.gameObject;
        _healBirdInstance.SetActive(false);
        _medkit = _healBirdInstance.GetComponent<Medkit>();

        _isSpawnEnabled = true;
        // Reset All Timers
        _carLocalTime = 0;
        ResetSpawnTimers();
        
        _currentSpawnStateIndex = 0;
        if(_testingMode)
        {
            _currentSpawnStateIndex = _startTestIndex;
        }
        ChangeSpawnLevel();
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

        if (_isCarSpawnPaused)
        {
            if (_carSpawnPauseLocalTime > _carSpawnPauseDuration)
            {
                _isCarSpawnPaused = false;
                _carSpawnPauseLocalTime = 0;
            }
            else
            {
                _carSpawnPauseLocalTime += Time.deltaTime;
                return;
            }
        }
        
        // Car
        TrySpawnCar();
        
        // State Delay
        if (_isDelayedSpawnState)
        {
            if (_globalSpawnLocalTime > _currentSpawnState.StateDelay)
            {
                _isDelayedSpawnState = false;
                ResetSpawnTimers();
            }
            else
            {
                _globalSpawnLocalTime += Time.deltaTime;
            }
        }
        else
        {
            if (_globalSpawnLocalTime < _currentSpawnState.StateDuration)
            {
                // Spawn Dogs
                if (_currentSpawnState.DogsEnabled)
                {
                    if (_isDogDelayed)
                    {
                        HandleEnemySpawnDelay(ref _isDogDelayed, ref _dogSpawnLocalTime, _currentSpawnState.DogSpawnDelay);
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
                            ref _isDogFirstSpawnInState
                            );
                    }
                }
                // Spawn Kangaroos
                if (_currentSpawnState.KangaroosEnabled)
                {
                    if (_isKangarooDelayed)
                    {
                        HandleEnemySpawnDelay(ref _isKangarooDelayed, ref _kangarooSpawnLocalTime, _currentSpawnState.KangarooSpawnDelay);
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
                // Spawn Birds
                if (_currentSpawnState.BirdsEnabled)
                {
                    if (_isBirdDelayed)
                    {
                        HandleEnemySpawnDelay(ref _isBirdDelayed, ref _birdSpawnLocalTime, _currentSpawnState.BirdSpawnDelay);
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
                _currentSpawnStateIndex++;
                ChangeSpawnLevel();
                ChangeSpawnState();
                _globalSpawnLocalTime = 0;
            }

            _globalSpawnLocalTime += Time.deltaTime;    
        }
    }

    private void HandleEnemySpawnDelay(ref bool isEnemyDelayed, ref float enemySpawnLocalTime, float enemySpawnDelay)
    {
        if (enemySpawnLocalTime > enemySpawnDelay)
        {
            isEnemyDelayed = false;
            enemySpawnLocalTime = 0;
        }
        enemySpawnLocalTime += Time.deltaTime;
    }
    
    /// <summary>
    /// Spawn Enemy from the Object Pool. References used to make it EnemyType Agnostic.
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
    
    private void TrySpawnCar()
    {
        if (_currentSpawnLevel == SpawnLevels.Beginning)
        {
            SpawnCar(_spawnCollection.CarSpawnTimeMinBeginning, _spawnCollection.CarSpawnTimeMaxBeginning);
        }
        else if (_currentSpawnLevel == SpawnLevels.Middle)
        {
            SpawnCar(_spawnCollection.CarSpawnTimeMinMiddle, _spawnCollection.CarSpawnTimeMaxMiddle);
        }
        else if (_currentSpawnLevel == SpawnLevels.Late)
        {
            SpawnCar(_spawnCollection.CarSpawnTimeMinLate, _spawnCollection.CarSpawnTimeMaxLate);
        }
    }

    private void SpawnCar(float min, float max)
    {
        if (_carLocalTime > _carSpawnRate)
        {
            _carEnemy.SetupSpawn(GetRandomDirection());
            _carLocalTime = 0f;
            _carSpawnRate = Random.Range(min, max);

            if (_currentSpawnLevel == SpawnLevels.Beginning)
            {
                _isCarSpawnPaused = true;
                _carSpawnPauseDuration = 2.9f;
                _carSpawnPauseLocalTime = 0;    
            }
            if (_currentSpawnLevel == SpawnLevels.Middle)
            {
                _isCarSpawnPaused = true;
                _carSpawnPauseDuration = 1.1f;
                _carSpawnPauseLocalTime = 0;    
            }
        }
        else
        {
            _carLocalTime += Time.deltaTime;
        }
    }

    private void ResetSpawnTimers()
    {
        _globalSpawnLocalTime = 0;
        _dogSpawnLocalTime = 0;
        _kangarooSpawnLocalTime = 0;
        _birdSpawnLocalTime = 0;
    }

    private void ChangeSpawnLevel()
    {
        if (_currentSpawnStateIndex <= _spawnCollection.GetLearnStateMaxIndex())
        {
            _currentSpawnLevel = SpawnLevels.Learn;
        }

        if (_currentSpawnStateIndex > _spawnCollection.GetLearnStateMaxIndex()
            && _currentSpawnStateIndex <= _spawnCollection.GetBeginningStateMaxIndex())
        {
            _currentSpawnLevel = SpawnLevels.Beginning;
        }
        
        if (_currentSpawnStateIndex > _spawnCollection.GetBeginningStateMaxIndex()
            && _currentSpawnStateIndex <= _spawnCollection.GetMiddleStateMaxIndex())
        {
            _currentSpawnLevel = SpawnLevels.Middle;
        }
        
        if (_currentSpawnStateIndex > _spawnCollection.GetMiddleStateMaxIndex()
            && _currentSpawnStateIndex <= _spawnCollection.GetLateStateMaxIndex())
        {
            _currentSpawnLevel = SpawnLevels.Late;
        }
    }
    
    private void GenerateRandomSpawnIndexForLevel(int count)
    {
        _prevSpawnStateRandomIndex = _currentSpawnStateRandomIndex;
        _currentSpawnStateRandomIndex = (int)Random.Range(0, count);
        if (_currentSpawnStateRandomIndex == _prevSpawnStateRandomIndex)
        {
            _currentSpawnStateRandomIndex = (int)Random.Range(0, count);
        }
    }
    private void ChangeSpawnState()
    {
        if (_currentSpawnLevel == SpawnLevels.Learn)
        {
            _currentSpawnState = _spawnCollection.SpawnStatesLearn[_currentSpawnStateIndex];
        }
        else if (_currentSpawnLevel == SpawnLevels.Beginning)
        {
            GenerateRandomSpawnIndexForLevel(_spawnCollection.SpawnStatesBeginning.Count);
            _currentSpawnState = _spawnCollection.SpawnStatesBeginning[_currentSpawnStateRandomIndex];
        }
        else if (_currentSpawnLevel == SpawnLevels.Middle)
        {
            GenerateRandomSpawnIndexForLevel(_spawnCollection.SpawnStatesMiddle.Count);
            _currentSpawnState = _spawnCollection.SpawnStatesMiddle[_currentSpawnStateRandomIndex];
        }
        else if (_currentSpawnLevel == SpawnLevels.Late)
        {
            GenerateRandomSpawnIndexForLevel(_spawnCollection.SpawnStatesLate.Count);
            _currentSpawnState = _spawnCollection.SpawnStatesLate[_currentSpawnStateRandomIndex];
        }
        
        _globalStateDir = GetRandomDirection();
        _dogGlobalStateDir = GetRandomDirection();
        _kangarooGlobalStateDir = GetRandomDirection();
        _birdGlobalStateDir = GetRandomDirection();
        
        _isDelayedSpawnState = _currentSpawnState.UseStateDelay;
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
        
        // TODO: remove this event from release version of the code.
        SpawnStateChanged?.Invoke(_currentSpawnState.name);
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
        _healBird.SetupSpawn(GetRandomDirection());
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
    
    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }
    
    private int GetRandomDirection()
    {
        return Random.Range(0, 2) * 2 - 1;
    }
}
