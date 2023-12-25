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
        PlayerInputHandler.OnStickPositionChanged += StickCheckValueAndVibrate;
        PlayerCollisionHandler.OnEnemyCollided += BlockHaptic;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnStickPositionChanged -= StickCheckValueAndVibrate;
        PlayerCollisionHandler.OnEnemyCollided -= BlockHaptic;
    }

    private void BlockHaptic()
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

    private void StickCheckValueAndVibrate(float xValue)
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
