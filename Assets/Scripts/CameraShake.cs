using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _shakeAmplitude;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private AnimationCurve _shakeProfileCurve;
    private Vector3 _originalPos;
    private float _prevTime;
    private bool _isShaking;
    
    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += StartShake;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= StartShake;
    }
    
    void Start()
    {
        _originalPos = transform.position;
        _isShaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShaking)
        {
            Shake();    
        }
    }

    private void Shake()
    {
        if (Time.time - _prevTime < _shakeDuration)
        {
            float shakeMask = _shakeProfileCurve.Evaluate((Time.time - _prevTime) / _shakeDuration);
            transform.position = Vector3.Lerp(_originalPos, _originalPos + (Vector3)(Random.insideUnitCircle * _shakeAmplitude), shakeMask);            
        }
        else
        {
            _isShaking = false;
        }
    }

    private void StartShake()
    {
        _prevTime = Time.time;
        _isShaking = true;
    }
    
}
