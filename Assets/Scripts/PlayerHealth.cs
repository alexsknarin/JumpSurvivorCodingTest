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
    public static event Action<int, Vector3> HealthIncreased;
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
        PlayerCollisionHandler.MedkitCollided += PlayerCollisionHandler_MedkitCollided;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.EnemyCollided -= PlayerCollisionHandler_EnemyCollided;
        BonusPointsManager.HealBonusReached -= BonusPointsManager_HealBonusReached;
        PlayerCollisionHandler.MedkitCollided -= PlayerCollisionHandler_MedkitCollided;
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

    private void IncreaseHealth(int mode, Vector3 pos)
    {
        if (_playerHealth.Value < _maxHealthCurrent.Value)
        {
            _playerHealth.Value++;
            CheckUpdateNearDeathState();
            HealthIncreased?.Invoke(mode, pos);
        }
    }
    private void BonusPointsManager_HealBonusReached()
    {
        IncreaseHealth(0, Vector3.zero);
    }
    
    private void CheckUpdateNearDeathState()
    {
        if (_playerHealth.Value < _nearDeathHealthNumber)
        {
            NearDeathStarted?.Invoke();
        }
        else
        {
            NearDeathEnded?.Invoke();
        }
    }

    private void PlayerCollisionHandler_MedkitCollided(Vector3 pos)
    {
        IncreaseHealth(1, pos);
    }
}