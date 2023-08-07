using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour, IPausable
{
    [SerializeField] private float _shakeAmplitude;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private AnimationCurve _shakeProfileCurve;
    [SerializeField] private FloatVariable _gameTime;
    private Vector3 _originalPos;
    private float _prevTime;
    private bool _isShaking;
    private bool _isPaused;
    
    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }
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
        Game.Pausables.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPaused)
        {
            if (_isShaking)
            {
                Shake();
            }
        }
    }

    private void Shake()
    {
        if (_gameTime.Value - _prevTime < _shakeDuration)
        {
            float shakeMask = _shakeProfileCurve.Evaluate((_gameTime.Value - _prevTime) / _shakeDuration);
            transform.position = Vector3.Lerp(_originalPos, _originalPos + (Vector3)(Random.insideUnitCircle * _shakeAmplitude), shakeMask);            
        }
        else
        {
            _isShaking = false;
        }            
    }

    private void StartShake()
    {
        _prevTime = _gameTime.Value;
        _isShaking = true;
    }
    
}
