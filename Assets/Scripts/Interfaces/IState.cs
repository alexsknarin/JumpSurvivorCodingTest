/// <summary>
/// Interface for all states for a state machines.
/// </summary>
public interface IState
{
    public void EnterState();
    public void ExecuteState();
    public void ExitState();
}