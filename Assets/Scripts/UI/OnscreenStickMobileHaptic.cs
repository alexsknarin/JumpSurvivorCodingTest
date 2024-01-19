using System;
using System.Collections;
using CandyCoded.HapticFeedback;
using UnityEngine;


public class OnscreenStickMobileHaptic : MonoBehaviour
{
    private bool _stickHapticMin = true;
    private bool _stickHapticMax = true;
    private float _inputXAbsValue;
    private bool _hapticAllowed = true;
    private WaitForSeconds _waitForHapticBlocked;

    private void Start()
    {
        _waitForHapticBlocked = new WaitForSeconds(0.8f);
    }

    private void OnEnable()
    {
        PlayerInputHandler.StickPositionChanged += PlayerInputHandler_StickPositionChanged;
        PlayerCollisionHandler.EnemyCollided += PlayerCollisionHandler_EnemyCollided;
    }

    private void OnDisable()
    {
        PlayerInputHandler.StickPositionChanged -= PlayerInputHandler_StickPositionChanged;
        PlayerCollisionHandler.EnemyCollided -= PlayerCollisionHandler_EnemyCollided;
    }

    private void PlayerCollisionHandler_EnemyCollided()
    {
        if (_hapticAllowed)
        {
            _hapticAllowed = false;
            StartCoroutine(WaitForHapticUnblock());            
        }
    }

    private IEnumerator WaitForHapticUnblock()
    {
        yield return _waitForHapticBlocked;
        AllowHaptic();
    }

    private void AllowHaptic()
    {
        _hapticAllowed = true;
    }

    private void PlayerInputHandler_StickPositionChanged(float xValue)
    {
        if (_hapticAllowed)
        {
            _inputXAbsValue = Mathf.Abs(xValue);
            // Medium Stick Haptics
            if (_stickHapticMin && (_inputXAbsValue > 0.65f))
            {
                HapticFeedback.LightFeedback();
                _stickHapticMin = false;
            }
        
            if(!_stickHapticMin && (_inputXAbsValue < 0.65f))
            {
                _stickHapticMin = true;
            }
        
            // Extreme Stick Haptics
            if (_stickHapticMax && (_inputXAbsValue > 0.95f))
            {
                HapticFeedback.HeavyFeedback();
                _stickHapticMax = false;
            }
        
            if(!_stickHapticMax && (_inputXAbsValue < 0.95f))
            {
                _stickHapticMax = true;
            }    
        }
    }
}
