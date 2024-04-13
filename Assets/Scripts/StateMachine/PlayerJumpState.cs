using UnityEngine;

/// <summary>
/// Control movement of the Player in a JUMP state.
/// </summary>
[CreateAssetMenu(menuName = "PlayerMovement/playerJumpState", fileName = "playerJumpState")]
public class PlayerJumpState : PlayerMovementBaseState
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _jumpHorizontalSpeed;
    [SerializeField] private float _airControl = 0.5f;
    private float _jumpDirection;
    private float _prevTime;

    public override void EnterState()
    {
        _prevTime = _gameTime.Value;
        _jumpDirection = 0;//_playerInputHandler.HorizontalAxis;
    }

    public override void ExecuteState()
    {
        if (_gameTime.Value - _prevTime < _jumpTime)
        {
            Vector3 jumpPos = _transform.position;
            float jumpPhase = (_gameTime.Value - _prevTime) / _jumpTime;
            jumpPos.y = _jumpCurve.Evaluate(jumpPhase) * _jumpHeight + 0.5f;
            jumpPos.x += _jumpDirection * _jumpHorizontalSpeed * Time.deltaTime; 
            _transform.position = jumpPos;
            // Air Movement
            float horizontalAxis = 0;//_playerInputHandler.HorizontalAxis; 
            float moveTranslate = horizontalAxis * _horizontalSpeed * _airControl * Time.deltaTime;
            _transform.Translate(Vector3.right * moveTranslate);

            _owner.ApplyBound(_transform);
            _owner.Speed = moveTranslate/0.15f;
            _owner.JumpPhase = jumpPhase;
        }
        else
        {
            _stateMachine.SetState(_owner.PlayerMoveState);
        }
    }
}