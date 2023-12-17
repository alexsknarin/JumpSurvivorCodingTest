using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlsPositionFit : MonoBehaviour
{
    [SerializeField] private Vector2 _referenceResolution;
    private float _referenceRatio;
    private float _currentRatio;
    private float _ratioMixValue;
    private const float LowestRatio = 0.62f;
    private Vector3 _localPosition;
    
    private void Start()
    {
        _localPosition = transform.localPosition;
        
        _referenceRatio = _referenceResolution.x / _referenceResolution.y;
        _currentRatio = (float)Screen.width / Screen.height;
        _ratioMixValue = _referenceRatio / _currentRatio;
        if (_ratioMixValue > 1)
        {
            _ratioMixValue = 1;
        }
        else
        {
            _ratioMixValue = 1 - (_ratioMixValue - LowestRatio) / (1f - LowestRatio);
        }

        _localPosition.y = Mathf.Lerp(0, 120, _ratioMixValue);

        transform.localPosition = _localPosition;
    }
}
