using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMovement/playerMoveState", fileName = "playerMoveState")]
public class PlayerMoveState : PlayerMovementBaseState
{
    [SerializeField] private float _horizontalSpeed;
    
    public override void EnterState()
    {
        Vector3 pos = _playerTransform.position;
        pos.y = 0.5f;
        _playerTransform.position = pos;
    }

    public override void ExecuteState()
    {
        _playerTransform.Translate(Vector3.right * Input.GetAxis("Horizontal") * _horizontalSpeed * Time.deltaTime);
        _owner.ApplyBound(_playerTransform);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _stateMachine.SetState(_owner._playerJumpState);
        }
    }
}