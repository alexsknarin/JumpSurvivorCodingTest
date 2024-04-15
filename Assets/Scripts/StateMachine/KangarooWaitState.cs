using UnityEngine;

/// <summary>
/// Control movement of the Kangaroo enemy in a WAIT state.
/// </summary>
[CreateAssetMenu(menuName = "EnemyMovement/KangarooWaitState", fileName = "kangarooWaitState")]
public class KangarooWaitState : KangarooMovementBaseState
{
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private float _waitTime;
    private float _prevTime;

    public override void EnterState()
    {
        Vector3 waitPos = _transform.position;
        waitPos.y = 1f;
        _transform.position = waitPos;
        _prevTime = _gameTime.Value;
    }
    
    public override void ExecuteState()
    {
        float deltaTime = _gameTime.Value - _prevTime;
        
        _owner.Speed = _direction;
        _owner.JumpPhase = 0f;
        
        if (deltaTime > _waitTime)
        {
            _stateMachine.SetState(_owner.KangarooJumpStateInstance);
            _owner.HandleJumpStart();
        }
    }
}
