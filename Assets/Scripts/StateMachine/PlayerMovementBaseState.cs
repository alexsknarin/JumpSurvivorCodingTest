using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBaseState : ScriptableObject, IState
{
    protected PlayerMovement _owner;
    protected StateMachine _stateMachine;
    protected Transform _playerTransform;
    
    public void Init(PlayerMovement owner, StateMachine stateMachine)
    {
        _owner = owner;
        _stateMachine = stateMachine;
        _playerTransform = owner.gameObject.transform;
    }

    public virtual void EnterState()
    {
    }

    public virtual void ExecuteState()
    {
    }

    public virtual void ExitState()
    {
    }
}