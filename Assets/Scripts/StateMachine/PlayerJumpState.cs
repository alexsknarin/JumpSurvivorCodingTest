using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _jumpDirection = Input.GetAxis("Horizontal");
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
            _transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * _horizontalSpeed * _airControl * Time.deltaTime);
                        
            _owner.ApplyBound(_transform);
        }
        else
        {
            _stateMachine.SetState(_owner._playerMoveState);
        }
    }
}