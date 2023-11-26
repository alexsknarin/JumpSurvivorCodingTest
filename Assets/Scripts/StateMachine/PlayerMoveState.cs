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
        float moveTranslate = Input.GetAxis("Horizontal") * _horizontalSpeed * Time.deltaTime;
        _transform.Translate(Vector3.right * moveTranslate);
        _owner.ApplyBound(_transform);
        _owner.Speed = moveTranslate/0.15f;
        _owner.JumpPhase = 0f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _stateMachine.SetState(_owner.PlayerJumpState);
        }
    }
}