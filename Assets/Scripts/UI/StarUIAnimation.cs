using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class StarUIAnimation : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    
    [SerializeField] private float _animDuration;
    [SerializeField] private float _animDistance;
    [SerializeField] private float _animDirection;
    [SerializeField] private float _scaleMultiplier = 1f;
    [SerializeField] private AnimationCurve _animMoveCurve;
    [SerializeField] private AnimationCurve _animRotateCurve;
    [SerializeField] private AnimationCurve _animScaleCurve;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _animStartTime; 
    private float _animCurrentTime;
    private float _animStartRotation;

    public void StartAnimation()
    {
        gameObject.SetActive(true);
        _animStartTime = Time.time;
        _startPos = transform.localPosition;
        _endPos = _startPos + (Quaternion.AngleAxis(_animDirection, Vector3.forward) * Vector3.right) * _animDistance;
        _animStartRotation = transform.localEulerAngles.z;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        _animCurrentTime = (Time.time - _animStartTime)/_animDuration;
        transform.localPosition = Vector3.Lerp(_startPos, _endPos, _animMoveCurve.Evaluate(_animCurrentTime));
        transform.localEulerAngles = Vector3.forward * (_animRotateCurve.Evaluate(_animCurrentTime) * 360) + Vector3.forward*_animStartRotation;
        transform.localScale = Vector3.one * (_animScaleCurve.Evaluate(_animCurrentTime) * _scaleMultiplier);
        if (_animCurrentTime > 1)
        {
            gameObject.SetActive(false);
            transform.localPosition = _startPos;
            transform.localEulerAngles = Vector3.forward * _animStartRotation;
        }
    }

    private void OnDrawGizmos()
    {
        if (_canvas)
        {
            Vector3 startPos = transform.localPosition;
            Vector3 direction = Vector3.right;
            direction = Quaternion.AngleAxis(_animDirection, Vector3.forward) * direction;
            Vector3 endPos = startPos + direction * _animDistance;
    
            Gizmos.matrix = _canvas.transform.localToWorldMatrix;
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(startPos, endPos);    
        }
    }
}
