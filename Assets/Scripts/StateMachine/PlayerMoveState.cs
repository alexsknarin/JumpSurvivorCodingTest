using UnityEngine;

/// <summary>
/// Control movement of the Player in a MOVE state.
/// </summary>
[CreateAssetMenu(menuName = "PlayerMovement/playerMoveState", fileName = "playerMoveState")]
public class PlayerMoveState : PlayerMovementBaseState
{
    [SerializeField] private float _horizontalSpeed;
    
    public override void EnterState()
    {
        Vector3 pos = _transform.position;
        pos.y = 0.5f;
        _transform.position = pos;
    }

    public override void ExecuteState()
    {
        float horizontalAxis = _playerInputHandler.HorizontalAxis; 
        float moveTranslate = horizontalAxis * _horizontalSpeed * Time.deltaTime;
        _transform.Translate(Vector3.right * moveTranslate);
        _owner.ApplyBound(_transform);
        _owner.Speed = moveTranslate/0.15f;
        _owner.JumpPhase = 0f;
        
        if (_playerInputHandler.JumpAction)
        {
            _stateMachine.SetState(_owner.PlayerJumpState);
            _owner.Jump();
        }
    }
}