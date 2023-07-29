using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerStates
{
    Move,
    Jump
}

public class PlayerMovement : MonoBehaviour
{
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

    private PlayerStates _playerStates;
    
    
    private void StartMove()
    {
        _boundedPos = transform.position;
        _boundedPos.y = 0.5f;
        transform.position = _boundedPos;
    }

    private void Move(float airControl)
    {
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * _speed * Time.deltaTime * airControl);  
    }

    private void ApplyBound()
    {
        _boundedPos = transform.position;
        if (transform.position.x < -_xBound)
        {
            _boundedPos.x = -_xBound;
            transform.position = _boundedPos;
        }
        else if (transform.position.x > _xBound)
        {
            _boundedPos.x = _xBound;
            transform.position = _boundedPos;
        }
    }

    private void Jump()
    {
        if (Time.time - _prevTime < _jumpTime)
        {
            Vector3 jumpPos = transform.position;
            float jumpPhase = (Time.time - _prevTime) / _jumpTime;
            jumpPos.y = _jumpCurve.Evaluate(jumpPhase) * _jumpHeight + 0.5f;
            jumpPos.x += _jumpDirection * _jumpHorizontalSpeed * Time.deltaTime; 
            transform.position = jumpPos;
            
            // Air Movement
            Move(_jumpControl);
        }
        else
        {
            _playerStates = PlayerStates.Move;
            StartMove();
        }
    }

    private void StartJump()
    {
        _prevTime = Time.time;
        _jumpDirection = Input.GetAxis("Horizontal");
        _playerStates = PlayerStates.Jump;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _playerStates = PlayerStates.Move;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_playerStates)
        {
            case PlayerStates.Move:
                Move(1f);
                break;
            case PlayerStates.Jump:
                Jump();
                break;
        }

        if ((_playerStates == PlayerStates.Move) && Input.GetKeyDown(KeyCode.Space))
        {
            StartJump();
        }
        
        //Jump();
        ApplyBound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }
    }
}
