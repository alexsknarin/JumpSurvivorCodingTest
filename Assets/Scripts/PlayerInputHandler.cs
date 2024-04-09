using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

/// <summary>
/// Watch for all Input related to the Player Character movement behaviour.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActionMap _playerInputActionMap;
    [SerializeField] private bool _applyLegacyGravity = true;
    [Tooltip("Same behaviour as in legacy Unity Horizontal axis")] 
    [SerializeField] private float _horizontalAxisGravity;
    private bool _isDrifting = false;
    private float _horizontalAxisInput;
    private int _currentDirection;
    private int _prevHorizontalDirection = 1;
    private float _horizontalInertia;
    private float _horizontalSensitiveInput;
    private bool _jumpButtonPressed = false;
    private InputDevice _currentInputDevice = new Keyboard();
    
    // events for extreme stick positions
    public float HorizontalAxis 
    {
        get
        {
            _horizontalAxisInput = _playerInputActionMap.PlayerMovement.Move.ReadValue<Vector2>().x;
            // if (_applyLegacyGravity && (_currentInputDevice.ToString() == "Keyboard:/Keyboard"))
            // {
            //     ApplyLegacyGravityHorizontalAxis();
            // }
            ApplyLegacyGravityHorizontalAxis();
            // Provide current stick state as an event with data
            StickPositionChanged?.Invoke(_horizontalAxisInput);
         
            return _horizontalAxisInput;
        }
    }
    public bool JumpAction => _jumpButtonPressed;
    
    public static event Action<float> StickPositionChanged;

    
    // TODO: initialize the input map from player manager
    void Awake()
    {
        _playerInputActionMap = new PlayerInputActionMap();
        _playerInputActionMap.PlayerMovement.Jump.started += context => { _jumpButtonPressed = true; };
        _playerInputActionMap.PlayerMovement.Jump.canceled += context => { _jumpButtonPressed = false; };
        _playerInputActionMap.PlayerMovement.Drift.started += context => { _isDrifting = true; };
        _playerInputActionMap.PlayerMovement.Drift.canceled += context => { _isDrifting = false; };
        InputSystem.onEvent += InputDeviceNameRead;
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
    
    void InputDeviceNameRead(InputEventPtr eventPtr, InputDevice device)
    {
        _currentInputDevice = device;
    }
    
    private void OnEnable()
    {
        _playerInputActionMap.Enable();
    }

    private void OnDisable()
    {
        _playerInputActionMap.Disable();
    }
}
