using System;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrail : MonoBehaviour
{
    [SerializeField] private Transform _inputTransform;
    [SerializeField] private LineRenderer _trailLineRenderer;
    [SerializeField] private AnimationCurve _trailWidthCurve;
    [SerializeField] private float _duration = 1.0f;
    private Material _material;
    
    private float _recordInterval = 0.02f;
    private int _recordStep = 0;
    private float _localRecordTime;
    private float _localTime;
    private Vector3[] _trailPositions = new Vector3[8];
    private float _startWidth = 0.15f;
    private float _endWidth = 1.5f;
    private bool _isRecording = false;
    private bool _isPlaying = false;

    private void Awake()
    {
        _material = GetComponent<LineRenderer>().material;
    }

    public void StartRecording()
    {
        gameObject.SetActive(true);
        _trailLineRenderer.positionCount = 0;
        for (int i = 0; i < _trailPositions.Length; i++)
        {
            _trailPositions[i] = Vector3.zero;
        }
        _recordStep = 0;
        _localRecordTime = 0;
        _localTime = 0;
        _isRecording = true;
        _isPlaying = true;
    }
    
    private void Update()
    {
        if (!_isPlaying)
        {
            return;
        }
        
        if (_isRecording)
        {
            if (_recordStep == 0)
            {
                _trailPositions[0] = _inputTransform.position;
                _trailPositions[0].y -= 0.5f;
                _recordStep++;    
            }
            
            if (_localRecordTime >= _recordInterval)
            {
                _trailPositions[_recordStep] = _inputTransform.position;
                _trailPositions[_recordStep].y -= 0.5f;

                if (_recordStep == 1)
                {
                    Vector3 dir = (_trailPositions[0] - _trailPositions[1]).normalized;
                    _trailPositions[0] += dir * 0.45f;
                }
                
                _recordStep++;
                
                if(_recordStep == _trailPositions.Length)
                {
                    _isRecording = false;
                    _recordStep = _trailPositions.Length - 1;
                    _localRecordTime = 0;
                    return;
                }

                _localRecordTime = 0;
            }
            
            _localRecordTime += Time.deltaTime;
        }
        
        float phase = _localTime / _duration;

        if (_recordStep > 0)
        {
            _trailLineRenderer.positionCount = _recordStep;
            for(int i = 0; i < _recordStep; i++)
            {
                _trailLineRenderer.SetPosition(i, _trailPositions[i]);
            }
        }
        
        if (phase >= 1)
        {
            _isPlaying = false;
            gameObject.SetActive(false);
            return;
        }

        _trailLineRenderer.widthCurve = _trailWidthCurve;
        _trailLineRenderer.widthMultiplier = Mathf.Lerp(_startWidth, _endWidth, phase);
        _material.SetFloat("_Dessipation", phase);
        
        _localTime += Time.deltaTime;
    }
}
