using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlusAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _duration;
    private float _prevTime;
    private bool _isAnimated = false;
    private float _animationPhase;

    public void Play()
    {
        _prevTime = Time.time;
        _isAnimated = true;
        transform.localScale = Vector3.one*0.05f;
    }

    private void Update()
    {
        if (_isAnimated)
        {
            _animationPhase = (Time.time - _prevTime) / _duration;
            if (_animationPhase > 1)
            {
                _isAnimated = false;
                gameObject.SetActive(false);
                return;
            }
            transform.localScale = Vector3.one * _animationCurve.Evaluate(_animationPhase);
        }
    }
}
