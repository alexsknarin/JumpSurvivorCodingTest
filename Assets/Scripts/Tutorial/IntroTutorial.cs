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
    private float _prevTime;

    private void Update()
    {
        if (_isTutorialActive)
        {
            PerformAnimation();
        }
    }
    
    public void Perform()
    {
        _isTutorialActive = true;
        _prevTime = Time.time;
    }
    
    private void PerformAnimation()
    {
        float animationPhase = (Time.time - _prevTime) / _duration;
        Color currentColor = Color.Lerp(_invisibleColor, _visibleColor, _animationCurve.Evaluate(animationPhase));
        _introText.color = currentColor;
        if (animationPhase > 1f)
        {
            gameObject.SetActive(false);
        }
    }


}
