using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private FloatVariable _gameTime;

    [Space(10)] 
    [SerializeField] private AnimationCurve _inflateAnimCurve;
    [SerializeField] private AnimationCurve _trembleAnimCurve;
    [SerializeField] private float _trembleFrequency;
    [SerializeField] private float _trembleAmplitude;
    [SerializeField] private float _checkpointAnimDuration;
    [SerializeField] private float _checkpointInterval;
    private float _animStartTime;
    private Vector3 _localRotation;
    private float _animCurrentTime = 0;
    private bool _isCheckpointAnimation = false;

    [SerializeField] private UnityEvent CheckpointReached;

    void Update()
    {
        ShowTimeText(FormatTime(_gameTime.Value));
      
        if (_isCheckpointAnimation)
        {
            PerformCheckpointAnimation();            
        }
        else
        {
            if (_gameTime.Value > _checkpointInterval
                && (int)_gameTime.Value % _checkpointInterval < 1)
            {
                StartCheckPointAnimation();
                CheckpointReached?.Invoke();
            }
        }
    }
    
    private void StartCheckPointAnimation()
    {
        _animStartTime = Time.time;
        _isCheckpointAnimation = true;
    }    
    private void PerformCheckpointAnimation()
    {
        _animCurrentTime = (Time.time - _animStartTime) / _checkpointAnimDuration;
        _localRotation = transform.localEulerAngles;
        _localRotation.z += Mathf.Sin(Time.time * _trembleFrequency) * _trembleAmplitude * _trembleAnimCurve.Evaluate(_animCurrentTime);
        transform.localEulerAngles = _localRotation;
        transform.localScale = Vector3.one * _inflateAnimCurve.Evaluate(_animCurrentTime);
        if (_animCurrentTime > _checkpointAnimDuration)
        {
            _isCheckpointAnimation = false;
        }
    }

    private void ShowTimeText(string time)
    {
        _timerText.text = time;
    }

    private string FormatTime(float time)
    {
        float minutes = (int)(time/60);
        float seconds = (int)(time%60);
        return minutes.ToString().PadLeft(2, '0') + ':' + seconds.ToString().PadLeft(2, '0');
    }
}