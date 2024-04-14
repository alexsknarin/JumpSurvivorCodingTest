using System;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrail : MonoBehaviour
{
    [SerializeField] private Transform _inputTransform;
    [SerializeField] private LineRenderer _trailLineRenderer;
    [SerializeField] private AnimationCurve _trailWidthCurve;
    private float _recordInterval = 0.02f;
    private int _recordStep = 0;
    private float _localTime;
    private Vector3[] _trailPositions = new Vector3[8];
    private float _initialStartWidth = 0.8f;
    private float _initialEndWidth = 0.2f;
    private bool _isRecording = false;

    private void OnEnable()
    {
        PlayerMovement.OnPlayerJump += StartRecording;
    }
    
    private void OnDisable()
    {
        PlayerMovement.OnPlayerJump -= StartRecording;
    }


    private void StartRecording()
    {
        _trailLineRenderer.positionCount = 0;
        for (int i = 0; i < _trailPositions.Length; i++)
        {
            _trailPositions[i] = Vector3.zero;
        }
        _recordStep = 0;
        _isRecording = true;
    }
    
    private void Update()
    {
        if (_isRecording)
        {
            if (_recordStep == 0)
            {
                _trailPositions[0] = _inputTransform.position;
                _trailPositions[0].y -= 0.5f;
                _recordStep++;    
            }
            
            if (_localTime >= _recordInterval)
            {
                _trailPositions[_recordStep] = _inputTransform.position;
                _trailPositions[_recordStep].y -= 0.5f;

                // if (_recordStep == 1)
                // {
                //     _trailPositions[1].x = (_trailPositions[1].x - _trailPositions[0].x) * 0.5f + _trailPositions[0].x; 
                // }
                
                _recordStep++;
                
                if(_recordStep == _trailPositions.Length)
                {
                    _isRecording = false;
                    _recordStep = _trailPositions.Length - 1;
                    _localTime = 0;
                    return;
                }

                _localTime = 0;
            }
            
            _localTime += Time.deltaTime;
        }

        if (_recordStep > 0)
        {
            _trailLineRenderer.positionCount = _recordStep;
            for(int i = 0; i < _recordStep; i++)
            {
                _trailLineRenderer.SetPosition(i, _trailPositions[i]);
            }
        }
        
        // _trailLineRenderer.startWidth = _initialStartWidth;
        // _trailLineRenderer.endWidth = _initialEndWidth;
        _trailLineRenderer.widthCurve = _trailWidthCurve;
    }
}
