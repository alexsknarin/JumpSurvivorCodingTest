using UnityEngine;

public class EnemyKangaroo : Enemy
{
    private StateMachine _moveStateMachine = new StateMachine();
    [SerializeField] private KangarooMovementBaseState _kangarooWaitState;
    [SerializeField] private KangarooMovementBaseState _kangarooJumpState;

    public KangarooMovementBaseState KangarooWaitStateInstance { get; private set; }
    public KangarooMovementBaseState KangarooJumpStateInstance { get; private set; }

    private void Awake()
    {
        // Creating Instances of the Scriptable objects - otherwise different enemies will use same variables messing them up
        KangarooWaitStateInstance = Instantiate(_kangarooWaitState);
        KangarooJumpStateInstance = Instantiate(_kangarooJumpState);
        KangarooWaitStateInstance.Init(this, _moveStateMachine);
        KangarooJumpStateInstance.Init(this, _moveStateMachine);
    }

    public override void SpawnSetup(float dir)
    {
        _spawnPos.x = 20f;
        _spawnPos.x *= -dir;
        _spawnPos.y = 1f;
        transform.position = _spawnPos;
        _direction = dir;
        
        KangarooWaitStateInstance.SetDirection(_direction);
        KangarooJumpStateInstance.SetDirection(_direction);
        _moveStateMachine.SetState(KangarooJumpStateInstance);

        this.gameObject.SetActive(true);

    }

    protected override void Move()
    {
        if (!_isPaused)
        {
            _moveStateMachine.Execute();
        }
    }
}
