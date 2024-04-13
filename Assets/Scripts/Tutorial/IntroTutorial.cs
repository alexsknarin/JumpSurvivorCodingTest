using TMPro;
using UnityEngine;

public class IntroTutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text _introText;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _duration;
    private Color _invisibleColor = new Color(1f, 1f, 1f, 0f);
    private Color _visibleColor = new Color(1f, 1f, 1f, 1f);
    private bool _isTutorialActive = false;
    private float _localTime = 0;

    
    private void Update()
    {
        if (_isTutorialActive)
        {
            PerformAnimation();
        }
    }
    
    public void Play()
    {
        _isTutorialActive = true;
        _localTime = 0;
    }
    
    private void PerformAnimation()
    {
        float phase = _localTime / _duration;
        Color currentColor = Color.Lerp(_invisibleColor, _visibleColor, _animationCurve.Evaluate(phase));
        _introText.color = currentColor;
        if (phase > 1f)
        {
            gameObject.SetActive(false);
        }
        _localTime += Time.deltaTime;
    }
}
