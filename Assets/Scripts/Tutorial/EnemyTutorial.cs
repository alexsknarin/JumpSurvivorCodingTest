using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{
    [SerializeField] GameObject _enemyTutorialObject;
    [SerializeField] GameObject _arrowTutorialObject;
    [SerializeField] GameObject _bonusTutorialObject;
    [SerializeField] private float _delay;
    [SerializeField] private float _duration;
    private TMP_Text _bonusTutorialText;
    private SpriteRenderer _kanagrooTutorialSpriteRenderer;
    private SpriteRenderer _arrowTutorialSpriteRenderer;
    private float _prevTime;
    private bool _isActive = false;
    private bool _isDelayPhase = true;
    private bool _isDissapearPhase = false;
    private bool _isBonusAnimated = false;
    private float _animationPhase;
    private Color _invisibleColor = new Color(1f, 1f, 1f, 0f);
    private Color _visibleColor = new Color(1f, 1f, 1f, 1f);
    
    private void Start()
    {
        _kanagrooTutorialSpriteRenderer = _enemyTutorialObject.GetComponent<SpriteRenderer>();
        _arrowTutorialSpriteRenderer = _arrowTutorialObject.GetComponent<SpriteRenderer>();
        _bonusTutorialText = _bonusTutorialObject.GetComponent<TMP_Text>();
        
        _enemyTutorialObject.SetActive(false);
        _arrowTutorialObject.SetActive(false);
        _bonusTutorialObject.SetActive(false);
    }

    public void Perform( int direction, int arrowDirection)
    {  
        _prevTime = Time.time;
        _enemyTutorialObject.SetActive(true);
        _arrowTutorialObject.SetActive(true);
        Vector3 dir = Vector3.one;
        dir.x = direction;
        transform.localScale = dir;
        dir.x = arrowDirection;
        _arrowTutorialObject.transform.localScale = dir * 0.6f;

        if (direction == -1)
        {
            _bonusTutorialObject.transform.eulerAngles = Vector3.up * 180f;
        }
        
        _isActive = true;
    }

    private void AppearAnimation()
    {
        _animationPhase = (Time.time - _prevTime) / _delay;
        Color currentColor = Color.Lerp(_invisibleColor, _visibleColor, _animationPhase);
        _kanagrooTutorialSpriteRenderer.color = currentColor; 
        _bonusTutorialText.color = currentColor;
    }
    
    private void DisappearAnimation()
    {
        _animationPhase = (Time.time - _prevTime) / _delay;
        Color currentColor = Color.Lerp(_visibleColor, _invisibleColor, _animationPhase);
        _kanagrooTutorialSpriteRenderer.color = currentColor; 
        _arrowTutorialSpriteRenderer.color = currentColor;
        _bonusTutorialText.color = currentColor;
    }
    
    private void BonusAnimation()
    {
        _animationPhase = (Time.time - _prevTime) / .5f;
        Color currentColor = Color.Lerp(_invisibleColor, _visibleColor, _animationPhase);
        _bonusTutorialText.color = currentColor;
        if (_animationPhase > 1f)
        {
            _isBonusAnimated = false;
        }
    }
    
    private void Update()
    {
        if (_isActive)
        {
            if (_isDelayPhase)
            {
                AppearAnimation();
                if(Time.time - _prevTime > _delay)
                {
                    _isDelayPhase = false;
                    _bonusTutorialObject.SetActive(true);
                    _prevTime = Time.time;
                    _isBonusAnimated = true;
                }
            }
            else if (_isDissapearPhase)
            {
                DisappearAnimation();
                if(Time.time - _prevTime > _delay)
                {
                    gameObject.SetActive(false);
                    _prevTime = Time.time;
                }
            }
            else
            {
                if (_isBonusAnimated)
                {
                    BonusAnimation();
                }
                if (Time.time - _prevTime > _duration)
                {
                    _isDissapearPhase = true;
                    _prevTime = Time.time;
                }
            }
        }
    }
}
