using System.Collections;
using UnityEngine;

/// <summary>
/// Component to perform a camera shake. Use Random.insideUnitCircle to get a random position each frame.
/// </summary>
public class CameraShake : MonoBehaviour, IPausable
{
    [SerializeField] private float _shakeAmplitude;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private AnimationCurve _shakeProfileCurve;
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private float _gameoverMultiplier;
    [SerializeField] private float _afterGameoverShakeDuration;
    private WaitForSeconds _afterGameoverShakeWait;
    
    private Vector3 _originalPos;
    private float _prevTime;
    private bool _isShaking;
    private bool _isPaused;
    private bool _isDamageable = true;
    private bool _isGameOver = false;
    
    private void Start()
    {
        _originalPos = transform.position;
        _isShaking = false;
        _afterGameoverShakeWait = new WaitForSeconds(_afterGameoverShakeDuration);
        Game.Pausables.Add(this);
    }
    
    private void Update()
    {
        if (!_isPaused || _isGameOver)
        {
            if (_isShaking)
            {
                PerformShake();
            }
        }
    }
    
    private void OnEnable()
    {
        PlayerCollisionHandler.EnemyCollided += PlayerCollisionHandler_EnemyCollided;
        PlayerHealth.PlayerInvincibilityFinished += PlayerHealth_PlayerInvincibilityFinished;
        Game.GameOver += Game_GameOver;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.EnemyCollided -= PlayerCollisionHandler_EnemyCollided;
        PlayerHealth.PlayerInvincibilityFinished -= PlayerHealth_PlayerInvincibilityFinished;
        Game.GameOver -= Game_GameOver;
    }
    
    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }
    
    /// <summary>
    /// Switch Camera Shake into a gameover mode with different amplitude.
    /// </summary>
    private void Game_GameOver()
    {
        _isGameOver = true;
        StartCoroutine(AfterGameOverShakeWait());
    }

    private IEnumerator AfterGameOverShakeWait()
    {
        yield return _afterGameoverShakeWait;
        AfterGameOverShakeStop();
    }
    
    private void AfterGameOverShakeStop()
    {
        _isGameOver = false;
    }

    /// <summary>
    /// Initialize Camera Shake
    /// </summary>
    private void PlayerCollisionHandler_EnemyCollided()
    {
        if (_isDamageable)
        {
            _prevTime = _gameTime.Value;
            _isShaking = true;
            _isDamageable = false;
        }
    }
    
    private void PerformShake()
    {
        if (_gameTime.Value - _prevTime < _shakeDuration)
        {
            float shakeMask = _shakeProfileCurve.Evaluate((_gameTime.Value - _prevTime) / _shakeDuration);
            float gameoverAmplitude = 1;
            if (_isGameOver)
            {
                gameoverAmplitude = _gameoverMultiplier;
            }
            transform.position = Vector3.Lerp(_originalPos, _originalPos + (Vector3)(Random.insideUnitCircle * (_shakeAmplitude * gameoverAmplitude)), shakeMask);            
        }
        else
        {
            _isShaking = false;
        }            
    }
    
    /// <summary>
    /// If Player is damageable again - allow camera to shake. While invincible camera will ignore enemy collisions.
    /// </summary>
    private void PlayerHealth_PlayerInvincibilityFinished()
    {
        _isDamageable = true;
    }
}
