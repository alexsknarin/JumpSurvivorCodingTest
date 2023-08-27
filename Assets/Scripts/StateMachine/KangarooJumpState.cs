using UnityEngine;

[CreateAssetMenu(menuName = "EnemyMovement/KangarooJumpState", fileName = "kangarooJumpState")]
public class KangarooJumpState : KangarooMovementBaseState
{
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpHorizontalSpeed;
    [SerializeField] private FloatVariable _gameTime;
    private float _prevTime;

    public override void EnterState()
    {
        _prevTime = _gameTime.Value;
    }

    public override void ExecuteState()
    {
        float deltaTime = _gameTime.Value - _prevTime;
        if (deltaTime < _jumpTime)
        {
            Vector3 jumpPos = _transform.position;
            float jumpPhase = (_gameTime.Value - _prevTime) / _jumpTime;
            jumpPos.y = _jumpCurve.Evaluate(jumpPhase) * _jumpHeight + 1f;
            jumpPos.x += _direction * _jumpHorizontalSpeed * Time.deltaTime; 
            _transform.position = jumpPos;
        }
        else
        {
            _stateMachine.SetState(_owner.KangarooWaitStateInstance);
        } 
    }
}
