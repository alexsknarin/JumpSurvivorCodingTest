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
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerInvincibilityFinished;
    

    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += DecreaseLives;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= DecreaseLives;
    }

    private void Start()
    {
        _maxHealthCurrent.Value = _maxHealth[_dificultyLevel.Value].Value;
        _playerHealth.Value = _maxHealthCurrent.Value;
        _waitForInvincibility = new WaitForSeconds(_invincibilityDuration);
    }

    private IEnumerator WaitForInvincibility()
    {
        yield return _waitForInvincibility;
        OnPlayerInvincibilityFinished?.Invoke();
        _isInvincible = false;
    }

    private void DecreaseLives()
    {
        if (!_isInvincible)
        {
            _playerHealth.Value -= 1;
            OnPlayerDamaged?.Invoke();
            _isInvincible = true;
            StartCoroutine(WaitForInvincibility());
        }
    }
}