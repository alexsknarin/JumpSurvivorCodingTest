/* Player Movement State with common functionality for all player movement states in the StateMachine.
 * TODO: possible make this class abstract 
 */
using UnityEngine;

/// <summary>
/// Base class for all Player movement states. 
/// </summary>
public class PlayerMovementBaseState : ScriptableObject, IState
{
    protected PlayerMovement _owner;
    protected StateMachine _stateMachine;
    protected Transform _transform;
    protected PlayerInputHandler _playerInputHandler;
    
    public void Init(PlayerMovement owner, StateMachine stateMachine)
    {
        _owner = owner;
        _stateMachine = stateMachine;
        _transform = owner.gameObject.transform;
        _playerInputHandler = _owner.GetComponent<PlayerInputHandler>();
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