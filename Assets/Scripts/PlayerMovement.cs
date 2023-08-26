using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerMovementStates
{
    Move,
    Jump
}

public class PlayerMovement : MonoBehaviour, IPausable
{
    [SerializeField] private FloatVariable _gameTime;
    private bool _isPaused;
    [SerializeField] private float _speed;
    [SerializeField] private float _xBound;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _jumpHorizontalSpeed;
    private float _jumpDirection;
    [SerializeField] private float _jumpControl;
    private float _prevTime;
    private Vector3 _boundedPos;
    
    //---
    private StateMachine _moveStateMachine = new StateMachine();
    public PlayerMovementBaseState _playerMoveState;
    public PlayerMovementBaseState _playerJumpState;
    
    // Start is called before the first frame update
    void Start()
    {
        Game.Pausables.Add(this);
        _playerMoveState.Init(this, _moveStateMachine);
        _playerJumpState.Init(this, _moveStateMachine);
        _moveStateMachine.SetState(_playerMoveState);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPaused)
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
