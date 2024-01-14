using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreNumbersAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _duration;
    [SerializeField] private float _bonusDuration;
    [SerializeField] private BonusPlusAnimation _bonusText;
    [SerializeField] private StarExplosion _starsVFX;

    [Header("Game Variables")] 
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private IntVariable _bonusPoints;
    
    private float _prevTime;
    private bool _isAnimated = false;
    private bool _isBonusAnimated = false;
    private bool _isExclamationAnimated = false;
    private float _animationPhase;
    private int _currentScore = 0;
    private int _maxScore =  300;
    private int _bonusScores = 55;
    private WaitForSeconds _bonusDelay;
    private int _maxExclamation = 3;
    private int _currExclamation = 0;

    public void StartAnimation()
    {
        _isAnimated = true;
        _prevTime = Time.time;
        _bonusDelay = new WaitForSeconds(0.25f);
        
        _maxScore =  (int)_gameTime.Value;
        _bonusScores = _bonusPoints.Value;
    }

    IEnumerator BonusDelay()
    {
        yield return _bonusDelay;
        _isBonusAnimated = true;
        _prevTime = Time.time;
        //_starsVFX.Play();
    }
    
    IEnumerator ExclamationDelay()
    {
        yield return _bonusDelay;
        _text.text += "!";
        _currExclamation++;
        _isExclamationAnimated = true;
    }

    private void Update()
    {
        if (_isAnimated)
        {
            _animationPhase = (Time.time - _prevTime) / _duration; 
            _currentScore = (int)Mathf.Lerp(0, _maxScore, _animationPhase);
            _text.text = _currentScore.ToString();
            if (_animationPhase > 1f)
            {
                _isAnimated = false;
                _text.text = _maxScore.ToString();
                _starsVFX.Play();
                if (_bonusScores > 0)
                {
                    _bonusText.gameObject.SetActive(true);
                    _bonusText.Play();  
                    StartCoroutine(BonusDelay());                    
                }
                else
                {
                    _isBonusAnimated = false;
                    _isExclamationAnimated = true;
                }
                
            }
        }
        
        if (_isBonusAnimated)
        {
            _animationPhase = (Time.time - _prevTime) / _bonusDuration; 
            _currentScore = (int)Mathf.Lerp(_maxScore, _maxScore+_bonusScores, _animationPhase);
            _text.text = _currentScore.ToString();
            if (_animationPhase > 1f)
            {
                _isBonusAnimated = false;
                _isExclamationAnimated = true;
            }
        }

        if (_isExclamationAnimated)
        {
            if (_currExclamation < _maxExclamation)
            {
                _isExclamationAnimated = false;
                StartCoroutine(ExclamationDelay());
            }
            else
            {
                _isExclamationAnimated = false;
                _starsVFX.Play();   
            }
        }
    }
}
