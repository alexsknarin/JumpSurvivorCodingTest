using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class StarUIAnimation : MonoBehaviour
{
    [SerializeField] private float _animDuration;
    [SerializeField] private float _animDistance;
    [SerializeField] private AnimationCurve _transformAnimCurve;
    private float _animStartTime;
    private float _animCurrentTime;
    private Vector3 _localPosition;
    private float _startXPos;
    void Start()
    {
        //gameObject.SetActive(false);
        _animStartTime = Time.time;
        _localPosition = transform.localPosition;
        _startXPos = _localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        _animCurrentTime = (Time.time - _animStartTime)/_animDuration; 
        _localPosition.x = Mathf.Lerp(0, _animDistance, _animCurrentTime) + _startXPos;
        transform.
        transform.localPosition = _localPosition;
        
    }
}
