using TMPro;
using UnityEngine;

public class BonusTutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text _bonusText;
    [SerializeField] private SpriteRenderer _heartSpriteRenderer;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _duration;
    private Color _invisibleColor = new Color(1f, 1f, 1f, 0f);
    private Color _visibleColor = new Color(1f, 1f, 1f, 1f);
    private bool _isTutorialActive = false;
    private float _prevTime;

    private void Start()
    {
        _bonusText.color = _invisibleColor;
        _heartSpriteRenderer.color = _invisibleColor;
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (_isTutorialActive)
        {
            PerformAnimation();
        }
    }
    
    public void Perform()
    {
        gameObject.SetActive(true);
        _isTutorialActive = true;
        _prevTime = Time.time;
    }
    
    private void PerformAnimation()
    {
        float animationPhase = (Time.time - _prevTime) / _duration;
        Color currentColor = Color.Lerp(_invisibleColor, _visibleColor, _animationCurve.Evaluate(animationPhase));
        _bonusText.color = currentColor;
        _heartSpriteRenderer.color = currentColor;
        if (animationPhase > 1f)
        {
            gameObject.SetActive(false);
        }
    }
}
