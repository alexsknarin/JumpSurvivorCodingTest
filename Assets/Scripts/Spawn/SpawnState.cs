/* Collection of variables that shape a spawn State - a bit of the enemy spawning sequence. 
 * Variables control things such as :
 * - Duration of the state.
 * - Delay before starting a current state.
 * - What enemies will be spawned with what rate and time offsets. 
 */
using UnityEngine;

/// <summary>
/// Describe a single spawnState.
/// </summary>
[CreateAssetMenu(menuName = "Spawn/SpawnState", fileName = "SpawnState")]
public class SpawnState : ScriptableObject
{
    [SerializeField] private int _stateId;
    public int StateId => _stateId;
    [SerializeField] private float _stateDuration;
    public float StateDuration => _stateDuration;
    [SerializeField] private bool _useStateDelay;
    public bool UseStateDelay => _useStateDelay;
    [SerializeField] private float _stateDelay;
    public float StateDelay => _stateDelay;

    [Header("---------------------------------")]
    
    [Header("Dogs:")]
    [SerializeField] private bool _dogsEnabled = false;
    public bool DogsEnabled => _dogsEnabled;
    [SerializeField] private bool _dogsRandomOncePerState = false;
    public bool DogsRandomOncePerState => _dogsRandomOncePerState;
    [SerializeField] private bool _dogsUseGlobalStateDirection = false;
    public bool DogsUseGlobalStateDirection => _dogsUseGlobalStateDirection;
    [SerializeField] private float _dogSpawnRate;
    public float DogSpawnRate => _dogSpawnRate;
    [SerializeField] private float _dogSpawnDelay;
    public float DogSpawnDelay => _dogSpawnDelay;
    
    [Header("Kangaroos:")]
    [SerializeField] private bool _kangaroosEnabled = false;
    public bool KangaroosEnabled => _kangaroosEnabled;
    [SerializeField] private bool _kangaroosRandomOncePerState = false;
    public bool KangaroosRandomOncePerState => _kangaroosRandomOncePerState;
    [SerializeField] private bool _kangaroosUseGlobalStateDirection = false;
    public bool KangaroosUseGlobalStateDirection => _kangaroosUseGlobalStateDirection;
    [SerializeField] private float _kangarooSpawnRate;
    public float KangarooSpawnRate => _kangarooSpawnRate;
    [SerializeField] private float _kangarooSpawnDelay;
    public float KangarooSpawnDelay => _kangarooSpawnDelay;
    
    [Header("Birds:")]
    [SerializeField] private bool _birdsEnabled = false;
    public bool BirdsEnabled => _birdsEnabled;
    [SerializeField] private bool _birdsRandomOncePerState = false;
    public bool BirdsRandomOncePerState => _birdsRandomOncePerState;
    [SerializeField] private bool _birdsUseGlobalStateDirection = false;
    public bool BirdsUseGlobalStateDirection => _birdsUseGlobalStateDirection;
    [SerializeField] private float _birdSpawnRate;
    public float BirdSpawnRate => _birdSpawnRate;
    [SerializeField] private float _birdSpawnDelay;
    public float BirdSpawnDelay => _birdSpawnDelay;
    
}
