using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFitResolution : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _referenceResolution;
    [SerializeField] private float _referenceCameraSize;
    private float _referenceRatio;
    private float _currentRatio;

    private void FitScreenToWidth()
    {
        _referenceRatio = _referenceResolution.x / _referenceResolution.y;
        _currentRatio = (float)Screen.width / Screen.height;
        _camera.orthographicSize = (_referenceCameraSize * _referenceRatio) / _currentRatio;
    }
    private void Start()
    {
        FitScreenToWidth();
    }
}
