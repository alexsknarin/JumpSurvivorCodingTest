using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerViewHandler : MonoBehaviour
{
    //[SerializeField] private int _direction = 1;
    [SerializeField] private GameObject _playerViewBase;
    private Vector3 _xFlip = Vector3.one;
    [SerializeField] private Animator _catBodyAnimator;
    [FormerlySerializedAs("_payerMovement")] [SerializeField] private PlayerMovement _playerMovement; 
    
    private void Update()
    {
        // Flip
        if (_playerMovement.Speed > 0)
        {
            _xFlip.x = 1;
        }
        else if (_playerMovement.Speed < 0)
        {
            _xFlip.x = -1;
        }
        _playerViewBase.transform.localScale = _xFlip;
        
        // Anim control
        _catBodyAnimator.SetFloat("speed", Mathf.Abs(_playerMovement.Speed));
        _catBodyAnimator.SetFloat("jumpPhase", _playerMovement.JumpPhase);
    }
}
