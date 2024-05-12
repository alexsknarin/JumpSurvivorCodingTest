using UnityEngine;

/// <summary>
/// Initialization, spawn procedure and Movement algorithm of a Kangaroo.
/// </summary>
public class EnemyKangaroo : Enemy
{
    [SerializeField] private EnemyTypes _enemyType;
    public override EnemyTypes EnemyType => _enemyType;
    
    private StateMachine _moveStateMachine = new StateMachine();
    [SerializeField] private KangarooMovementBaseState _kangarooWaitState;
    [SerializeField] private KangarooMovementBaseState _kangarooJumpState;
    [SerializeField] private EnemyKangarooViewHandler _kangarooViewHandler;

    public float Speed { get; set; }
    public float JumpPhase { get; set; }

    public KangarooMovementBaseState KangarooWaitStateInstance { get; private set; }
    public KangarooMovementBaseState KangarooJumpStateInstance { get; private set; }

    private void Awake()
    {
        // Creating Instances of the ScriptableObjects, or different enemies will use the same variables messing them up
        KangarooWaitStateInstance = Instantiate(_kangarooWaitState);
        KangarooJumpStateInstance = Instantiate(_kangarooJumpState);
        KangarooWaitStateInstance.Init(this, _moveStateMachine);
        KangarooJumpStateInstance.Init(this, _moveStateMachine);
    }

    public override void SetupSpawn(float dir, int lvl)
    {
        _spawnPos.x = 15.05f;
        _spawnPos.x *= -dir;
        _spawnPos.y = 1f;
        transform.position = _spawnPos;
        _direction = dir;
       
        KangarooWaitStateInstance.SetDirection(_direction);
        KangarooJumpStateInstance.SetDirection(_direction);
        _moveStateMachine.SetState(KangarooJumpStateInstance);
    
        gameObject.SetActive(true);
        _kangarooViewHandler.Initialize((int)_direction);
        _kangarooViewHandler.HandleJumpStart();
    }

    protected override void Move()
    {
        if (!_isPaused)
        {
            _moveStateMachine.Execute();
        }
    }
    
    public void HandleJumpStart()
    {
        _kangarooViewHandler.HandleJumpStart();
    }
    
    public void HandleJumpEnd()
    {
        _kangarooViewHandler.HandleJumpEnd();
    }
}
