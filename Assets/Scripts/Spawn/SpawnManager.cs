using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [Header("Enemies:")]
    [SerializeField] private GameObject _enemyDog;
    [SerializeField] private int _maximumDogs;
    [SerializeField] private GameObject _enemyKangaroo;
    [SerializeField] private int _maximumKangaroos;
    [SerializeField] private GameObject _enemyBird;
    [SerializeField] private int _maximumBirds;

    [Header("Spawn States")] 
    [SerializeField] private SpawnStateCollection _spawnCollection;
    private SpawnState _currentSpawnState;
    private int _currentSpawnStateIndex = 0;
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
    
    private float _globalStateDir;
    private float _dogGlobalStateDir;
    private float _kangarooGlobalStateDir;
    private float _birdGlobalStateDir;
    private float _currentEnemyDir;
    
    private bool _delayMode = false;
    
    // State Debug UI
    public delegate void SpawnStateChanged(string StateName);
    public static event SpawnStateChanged OnSpawnStateChanged;
    
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
            _currentSpawnStateIndex = (int)Random.Range(0, _spawnCollection.SpawnStatesMainLoop.Count);
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
                       _currentSpawnState.DogsRandomOncePerState, _currentSpawnState.DogsUseGlobalStateDirection);
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
                       _currentSpawnState.KangaroosRandomOncePerState, _currentSpawnState.KangaroosUseGlobalStateDirection);
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
                       _currentSpawnState.BirdsRandomOncePerState, _currentSpawnState.BirdsUseGlobalStateDirection);
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
                _isLearningPhase = false;
                ChangeSpawnState();
            }
        }
        
    }

    private void SpawnEnemy(ObjectPool enemyPool, ref float enemySpawnPrevTime, float spawnRate, 
        float enemyGlobalDir, bool useEnemyGlobal, bool useStateGlobal)
    {
        if (Time.time - enemySpawnPrevTime > spawnRate)
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
        }
    }
}
