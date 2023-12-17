using CandyCoded.HapticFeedback;
using UnityEngine;

public class PlayerMovementBaseState : ScriptableObject, IState
{
    protected PlayerMovement _owner;
    protected StateMachine _stateMachine;
    protected Transform _transform;
    protected PlayerInputHandler _playerInputHandler;
    
    // Mobile Haptics
    private bool _stickHapticMin = true;
    private bool _stickHapticMax = true;
    
    
    public void Init(PlayerMovement owner, StateMachine stateMachine)
    {
        _owner = owner;
        _stateMachine = stateMachine;
        _transform = owner.gameObject.transform;
        _playerInputHandler = _owner.GetComponent<PlayerInputHandler>();
    }

    protected void MobileMoveStickHapticPerform(float value)
    {
        // Medium Stick Haptics
        if (_stickHapticMin && (value > 0.65f))
        {
            HapticFeedback.LightFeedback();
            _stickHapticMin = false;
        }
        
        if(!_stickHapticMin && (value < 0.65f))
        {
            _stickHapticMin = true;
        }
        
        // Extreme Stick Haptics
        if (_stickHapticMax && (value > 0.95f))
        {
            HapticFeedback.HeavyFeedback();
            _stickHapticMax = false;
        }
        
        if(!_stickHapticMax && (value < 0.95f))
        {
            _stickHapticMax = true;
        }   
    }

    protected void MobileJumpButtonHapticPerform()
    {
        HapticFeedback.HeavyFeedback();
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