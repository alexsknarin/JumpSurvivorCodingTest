using TMPro;
using UnityEngine;

public class BonusPointsView : MonoBehaviour, IPausable
{
    [SerializeField] private RectTransform _bonusTextTransform;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _duration = 3f;
    [SerializeField] private AnimationCurve _animationSpeedCurve; 
    private bool _isAnimated = false;
    private float _animationPhase = 0f;
    private float _prevTime = 0f;
    private Vector3 _translateDirection;
    
    public int CurrentBonusPoints { get; set; }
    
    private void Update()
    {
        if (_isAnimated)
        {
            Animation();
        }
    }

    public void SetPaused()
    {
    }

    public void SetUnpaused()
    {
    }

    public void SpawnSetup(int pointNum, Vector2 startScreenPosition, RectTransform bonusTextTransform)
    {
        gameObject.SetActive(true);
        Debug.Log(transform.position);
        Debug.Log(startScreenPosition);
        transform.position = startScreenPosition;
        Debug.Log(transform.position);
        Debug.Log(transform.localPosition);
        Debug.Break();
        _bonusTextTransform = bonusTextTransform;
        _text.text = $"+ {pointNum}";
        _isAnimated = true;
        _animationPhase = 0f;
        _prevTime = Time.time;
    }

    private void Animation()
    {
        _animationPhase = (Time.time - _prevTime)/_duration;

        if (_animationPhase < 0.14f)
        {
            _translateDirection = Vector3.up;
        }
        else
        {
            _translateDirection = (_bonusTextTransform.transform.position - transform.position);
        }
        transform.Translate(_translateDirection.normalized * (_speed * Time.deltaTime * _animationSpeedCurve.Evaluate(_animationPhase)));
    }
}
