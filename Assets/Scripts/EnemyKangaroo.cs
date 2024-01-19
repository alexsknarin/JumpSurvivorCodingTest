using UnityEngine;

/// <summary>
/// Initialization, spawn procedure and Movement algorithm of a Kangaroo.
/// </summary>
public class EnemyKangaroo : Enemy
{
    private StateMachine _moveStateMachine = new StateMachine();
    [SerializeField] private KangarooMovementBaseState _kangarooWaitState;
    [SerializeField] private KangarooMovementBaseState _kangarooJumpState;
    public override string EnemyName => "Kangaroo";
    
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

    public override void SetupSpawn(float dir)
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
    }

    protected override void Move()
    {
        if (!_isPaused)
        {
            _moveStateMachine.Execute();
        }
    }
}
