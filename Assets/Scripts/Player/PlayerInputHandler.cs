using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Watch for all Input related to the Player Character movement behaviour.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerInputActionMap;
    [SerializeField] private string _actionMapName = "PlayerMovement";
    [SerializeField] private string _actionMoveName = "Move";
    [SerializeField] private string _actionDriftName = "Drift";
    [SerializeField] private string _actionJumpName = "Jump";
    private InputAction _moveAction;
    private InputAction _driftAction;
    private InputAction _jumpAction;
    
    // Move
    private readonly float _horizontalAxisGravity = 3f;
    private float _horizontalAxisInput;
    private bool _isDrifting = false;
    private int _currentDirection;
    private int _prevHorizontalDirection = 1;
    private float _horizontalInertia;
    private float _horizontalSensitiveInput;
    public float HorizontalAxis 
    {
        get
        {
            _horizontalAxisInput = _moveAction.ReadValue<Vector2>().x;
            ApplyLegacyGravityHorizontalAxis();
            return _horizontalAxisInput;
        }
    }
    
    // Jump
    private bool _jumpButtonPressed = false;
    public bool JumpAction => _jumpButtonPressed;
    

    private void Awake()
    {
        _moveAction = _playerInputActionMap.FindActionMap(_actionMapName).FindAction(_actionMoveName);
        _driftAction = _playerInputActionMap.FindActionMap(_actionMapName).FindAction(_actionDriftName);
        _jumpAction = _playerInputActionMap.FindActionMap(_actionMapName).FindAction(_actionJumpName);
        
        _driftAction.performed += DriftPerformed;
        _driftAction.canceled += DriftCanceled;
        
        _jumpAction.performed += JumpPerformed;
        _jumpAction.canceled += JumpCanceled;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _driftAction.Enable();
        _jumpAction.Enable();
    }
    
    private void OnDisable()
    {
        _moveAction.Disable();
        _driftAction.Disable();
        _jumpAction.Disable();
    }
    
    private void DriftPerformed(InputAction.CallbackContext context)
    {
        _isDrifting = true;
    }
    
    private void DriftCanceled(InputAction.CallbackContext context)
    {
        _isDrifting = false;
    }
    
    private void JumpPerformed(InputAction.CallbackContext context)
    {
        _jumpButtonPressed = true;
    }
    
    private void JumpCanceled(InputAction.CallbackContext context)
    {
        _jumpButtonPressed = false;
    }
    
    private void ApplyLegacyGravityHorizontalAxis()
    {
        _currentDirection = (int)Mathf.Sign(_horizontalAxisInput);
        if (!_isDrifting)
        {
            _horizontalSensitiveInput = _horizontalAxisInput;
            
            // Applying Inertia
            if (Mathf.Approximately(_horizontalAxisInput, 0))
            {
                //Decceleration - No button Pressed
                // keep the same direction unless it changed
                _currentDirection = _prevHorizontalDirection;
                if (_horizontalInertia > 0)
                {
                    _horizontalInertia -= _horizontalAxisGravity * Time.deltaTime;
                    _horizontalSensitiveInput = _prevHorizontalDirection * _horizontalInertia;    
                }
                if (_horizontalInertia < 0)
                {
                    _horizontalInertia = 0;
                    _horizontalSensitiveInput = 0;
                }
            }
            else
            {
                // Acceleration - one button pressed
                if (_horizontalInertia < 1f)
                {
                    _horizontalInertia += _horizontalAxisGravity * Time.deltaTime;
                }
                        
                if (_horizontalInertia >= 1f)
                {
                    _horizontalInertia = 1f;
                }
                _horizontalSensitiveInput *= _horizontalInertia;
            }
        }
        
        _horizontalAxisInput = _horizontalSensitiveInput;
        
        if ((_currentDirection != _prevHorizontalDirection) && (!_isDrifting) && (_prevHorizontalDirection != 0))
        {
            _prevHorizontalDirection = _currentDirection;
            _horizontalInertia = 0;
            _horizontalSensitiveInput = 0;
        }
    }
}
