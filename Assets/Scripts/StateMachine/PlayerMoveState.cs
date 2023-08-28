using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * _horizontalSpeed * Time.deltaTime);
        _owner.ApplyBound(_transform);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _stateMachine.SetState(_owner.PlayerJumpState);
        }
    }
}