using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manage player's amount of health. Provides events for heals amount changes.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private IntVariable[] _maxHealth;
    [SerializeField] private IntVariable _maxHealthCurrent;
    [SerializeField] private IntVariable _dificultyLevel;
    [SerializeField] private float _invincibilityDuration;
    [SerializeField] private float _nearDeathFraction;
    private float _nearDeathHealthNumber;
    private bool _isInvincible = false;
    private WaitForSeconds _waitForInvincibility;
    
    public static event Action HealthDecreased;
    public static event Action PlayerInvincibilityFinished;
    public static event Action PlayerHealthSetUp;
    public static event Action HealthIncreased;
    public static event Action NearDeathStarted;
    public static event Action NearDeathEnded;

    private void Start()
    {
        _maxHealthCurrent.Value = _maxHealth[_dificultyLevel.Value].Value;
        _playerHealth.Value = _maxHealthCurrent.Value;
        _waitForInvincibility = new WaitForSeconds(_invincibilityDuration);
        _nearDeathHealthNumber = _maxHealthCurrent.Value * _nearDeathFraction;
        PlayerHealthSetUp?.Invoke();
    }

    private void OnEnable()
    {
        PlayerCollisionHandler.EnemyCollided += PlayerCollisionHandler_EnemyCollided;
        BonusPointsManager.HealBonusReached += BonusPointsManager_HealBonusReached;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.EnemyCollided -= PlayerCollisionHandler_EnemyCollided;
        BonusPointsManager.HealBonusReached -= BonusPointsManager_HealBonusReached;
    }

    private IEnumerator WaitForInvincibility()
    {
        yield return _waitForInvincibility;
        PlayerInvincibilityFinished?.Invoke();
        _isInvincible = false;
    }

    /// <summary>
    /// Decrease Health
    /// </summary>
    private void PlayerCollisionHandler_EnemyCollided()
    {
        if (!_isInvincible)
        {
            _playerHealth.Value -= 1;
            CheckUpdateNearDeathState();
            HealthDecreased?.Invoke();
            _isInvincible = true;
            StartCoroutine(WaitForInvincibility());
        }   
    }

    /// <summary>
    /// Increase Health
    /// </summary>
    private void BonusPointsManager_HealBonusReached()
    {
        if (_playerHealth.Value < _maxHealthCurrent.Value)
        {
            _playerHealth.Value++;
            CheckUpdateNearDeathState();
            HealthIncreased?.Invoke();
        }
    }
    
    private void CheckUpdateNearDeathState()
    {
        Debug.Log("Current health is: ");
        Debug.Log(_playerHealth.Value);
        Debug.Log("NearDeath is expected on    : ");
        Debug.Log(_nearDeathHealthNumber);
        
        if (_playerHealth.Value < _nearDeathHealthNumber)
        {
            Debug.Log("Near Death Start");
            NearDeathStarted?.Invoke();
        }
        else
        {
            Debug.Log("Near Death Stop");
            NearDeathEnded?.Invoke();
        }
    }
}