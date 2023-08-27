using UnityEngine;

public class MovementBaseState : ScriptableObject, IState
{
    protected PlayerMovement _owner;
    protected StateMachine _stateMachine;
    protected Transform _transform;
    
    public void Init(PlayerMovement owner, StateMachine stateMachine)
    {
        _owner = owner;
        _stateMachine = stateMachine;
        _transform = owner.gameObject.transform;
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