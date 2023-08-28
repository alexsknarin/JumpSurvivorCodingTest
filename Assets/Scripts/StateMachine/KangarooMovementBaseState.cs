using UnityEngine;

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