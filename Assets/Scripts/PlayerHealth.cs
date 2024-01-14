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
    private bool _isInvincible = false;
    private WaitForSeconds _waitForInvincibility;
    public static event Action HealthDecreased;
    public static event Action PlayerInvincibilityFinished;
    public static event Action PlayerHealthSetUp;
    public static event Action HealthIncreased;

    private void Start()
    {
        _maxHealthCurrent.Value = _maxHealth[_dificultyLevel.Value].Value;
        _playerHealth.Value = _maxHealthCurrent.Value;
        _waitForInvincibility = new WaitForSeconds(_invincibilityDuration);
        PlayerHealthSetUp?.Invoke();
    }

    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += OnDecreaseHealth;
        BonusPointsManager.OnHeal += OnIncreaseHealth;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= OnDecreaseHealth;
        BonusPointsManager.OnHeal -= OnIncreaseHealth;
    }

    private IEnumerator WaitForInvincibility()
    {
        yield return _waitForInvincibility;
        PlayerInvincibilityFinished?.Invoke();
        _isInvincible = false;
    }

    private void OnDecreaseHealth()
    {
        if (!_isInvincible)
        {
            _playerHealth.Value -= 1;
            HealthDecreased?.Invoke();
            _isInvincible = true;
            StartCoroutine(WaitForInvincibility());
        }   
    }

    private void OnIncreaseHealth()
    {
        if (_playerHealth.Value < _maxHealthCurrent.Value)
        {
            _playerHealth.Value++;
            HealthIncreased?.Invoke();
        }
    }
}