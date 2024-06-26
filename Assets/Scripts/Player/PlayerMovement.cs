using System;
using UnityEngine;

/// <summary>
/// Managing movement State machine for a Player character.
/// </summary>
public class PlayerMovement : MonoBehaviour, IPausable
{
    private bool _isPaused;
    private bool _isInitialized = false;
    [SerializeField] private float _xBound;
    private Vector3 _boundedPos;

    private StateMachine _moveStateMachine = new StateMachine();
    // Those fields are public to be accessible from the state machine
    public PlayerMovementBaseState PlayerMoveState;
    public PlayerMovementBaseState PlayerJumpState;
    
    // Animator Hooks
    public float Speed { get; set; }
    public float JumpPhase { get; set; }
    
    public static event Action OnPlayerJump;
    
    public void Jump()
    {
        OnPlayerJump?.Invoke();
    }

    public void Initialize()
    {
        Game.Pausables.Add(this);
        PlayerMoveState.Init(this, _moveStateMachine);
        PlayerJumpState.Init(this, _moveStateMachine);
        _moveStateMachine.SetState(PlayerMoveState);
        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isPaused && _isInitialized)
        {
            _moveStateMachine.Execute();
        }
    }

    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }

    public void ApplyBound(Transform xform)
    {
        Vector3 boundedPos = xform.position;
        if (xform.position.x < -_xBound)
        {
            boundedPos.x = -_xBound;
            xform.position = boundedPos;
        }
        else if (xform.position.x > _xBound)
        {
            boundedPos.x = _xBound;
            xform.position = boundedPos;
        }
    }
}
