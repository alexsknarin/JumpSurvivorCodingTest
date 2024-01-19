/* State Machine that is used by all characters in the game to control movement states.
 * Each state implements IState interface but is also a scriptable object, which allows to create states as assets
 * and setting them up in the inspector.
 */

/// <summary>
/// Abstract state machine.
/// </summary>
public class StateMachine
{
    private IState _currentState;

    public void SetState(IState state)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }
        _currentState = state;
        _currentState.EnterState();
    }

    public void Execute()
    {
        _currentState.ExecuteState();
    }
}