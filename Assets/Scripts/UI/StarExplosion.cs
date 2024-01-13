using System.Collections.Generic;
using UnityEngine;

class StarAnimData
{
    public float Scale { get; set; }
    public Vector3 StartPos { get; set; }
    public Vector3 EndPos { get; set; }
    public int RotateDirection { get; set; }
}

public class StarExplosion : MonoBehaviour, IPausable
{
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private List<Transform> _stars;
    [SerializeField] private float _emitRadius;
    [SerializeField] private float _maxDistance;
    [SerializeField] private AnimationCurve _animMoveCurve;
    [SerializeField] private AnimationCurve _animScaleCurve;
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private float _animDuration;
    [SerializeField] private float _rotationSpeed;
   
    private List<StarAnimData> _starsAnimData;
    private float _prevTime;
    private float _animPhase;
    private bool _isAnimated = false;

    public void Play()
    {
        for(int i = 0; i < _stars.Count; i++)
        {
            _stars[i].localScale = Vector3.zero;
            float startScale = Random.Range(_minSize, _maxSize);
            
            Vector3 startPos = Random.insideUnitCircle * _emitRadius;
            _stars[i].localPosition = startPos;
            Vector3 endPos = CheckIfRandomInQuarter(i,Random.insideUnitCircle).normalized * _maxDistance;
            endPos *= Mathf.Lerp(startScale / _maxSize, 0.6f, 1f) * 1.4f;

            _starsAnimData[i].Scale = startScale;
            _starsAnimData[i].StartPos = startPos;
            _starsAnimData[i].EndPos = endPos;
            _starsAnimData[i].RotateDirection = Random.Range(0, 2) * 2 - 1;

            _prevTime = Time.time;
            _isAnimated = true;
        }
        gameObject.SetActive(true);
    }

    private void PerformAnimation()
    {
        _animPhase = (Time.time - _prevTime) / _animDuration;
        if (_animPhase > 1)
        {
            _isAnimated = false;
            gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < _stars.Count; i++)
        {
            _stars[i].localPosition = Vector3.Lerp(_starsAnimData[i].StartPos, _starsAnimData[i].EndPos, _animMoveCurve.Evaluate(_animPhase));
            _stars[i].localScale = Vector3.one * (_starsAnimData[i].Scale * _animScaleCurve.Evaluate(_animPhase));
            _stars[i].localEulerAngles += Vector3.forward * (_rotationSpeed * Time.deltaTime * _starsAnimData[i].RotateDirection);
        }
    }

    public static Vector3 CheckIfRandomInQuarter(int quarter, Vector3 randomPoint)
    {
        if (quarter == 0)
        {
            randomPoint.x = -Mathf.Abs(randomPoint.x);
            randomPoint.y = Mathf.Abs(randomPoint.y);
        }
        if (quarter == 1)
        {
            randomPoint.x = Mathf.Abs(randomPoint.x);
            randomPoint.y = Mathf.Abs(randomPoint.y);
        }
        if (quarter == 2)
        {
            randomPoint.x = -Mathf.Abs(randomPoint.x);
            randomPoint.y = -Mathf.Abs(randomPoint.y);
        }
        if (quarter == 3)
        {
            randomPoint.x = Mathf.Abs(randomPoint.x);
            randomPoint.y = -Mathf.Abs(randomPoint.y);
        }
        return randomPoint;
    }
   
    private void Awake()
    {
        _starsAnimData = new List<StarAnimData>();
        for (int i = 0; i < _stars.Count; i++)
        {
            _starsAnimData.Add(new StarAnimData());    
        }
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isAnimated)
        {
            PerformAnimation();
        }
    }
    
    
    private void OnDrawGizmos()
    {
        if (_canvas)
        {
            Vector3 centerPos = transform.localPosition;            
            Gizmos.matrix = _canvas.transform.localToWorldMatrix;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(centerPos, _emitRadius);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(centerPos, _maxDistance);
        }
    }

    public void SetPaused()
    {
    }

    public void SetUnpaused()
    {
    }
}
