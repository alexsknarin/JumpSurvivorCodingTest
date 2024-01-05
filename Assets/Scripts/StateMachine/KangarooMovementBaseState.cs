using UnityEngine;

/// <summary>
/// Base class for all Kangaroo movement states. 
/// </summary>
public class KangarooMovementBaseState : ScriptableObject, IState
{
    
    protected EnemyKangaroo _owner;
    protected StateMachine _stateMachine;
    protected Transform _transform;
    protected float _direction;

    public void SetDirection(float direction)
    {
        _direction = direction;
    }

    /// <summary>
    /// Initialize of the Kangaroo movement state. Shared between all Kangaroo movement states.
    /// </summary>
    /// <param name="owner">Reference to a current EnemyKangaroo object.</param>
    /// <param name="stateMachine">Reference to a current StateMachine object.</param>
    public void Init(EnemyKangaroo owner, StateMachine stateMachine)
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