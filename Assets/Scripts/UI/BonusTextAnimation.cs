using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BonusTextAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private float _animDuration;
    private float _prevTime;
    private float _currentPhase;
    private bool _isAnimated = false;

    public void PlayAnimation()
    {
        _isAnimated = true;
        _prevTime = Time.time;
    }
    
    private void ScaleAnimation()
    {
        _currentPhase = (Time.time - _prevTime) / _animDuration;
        if (_currentPhase > 1f)
        {
            _isAnimated = false;
            transform.localScale = Vector3.one;
            return;
        }
        
        transform.localScale = Vector3.one * _animCurve.Evaluate(_currentPhase);
    }

    private void Update()
    {
        if (_isAnimated)
        {
            ScaleAnimation();
        }
    }
}
