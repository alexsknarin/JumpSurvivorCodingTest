using System;
using UnityEngine;

/// <summary>
/// Watch game events and generate bonus points. Generate heal event every N points
/// </summary>
public class BonusPointsManager : MonoBehaviour
{
    [SerializeField] private IntVariable _bonusPoints;
    [SerializeField] private int _kangarooBonus;
    [SerializeField] private int _birdBonus;
    [SerializeField] private int _healEveryNPoints; 
    private bool _bonusRegisterd = false;
    private int _bonusEnemy;
    private Vector3 _bonusPosition;
    private bool _isDamaged = false;
    private int _currentBonus;
    private int _healCount = 0;

    public static event Action<int, Vector3> BonusUpdated;
    public static event Action HealBonusReached;

    private void OnEnable()
    {
        PlayerCollisionHandler.BonusCollided += PlayerCollisionHandler_BonusCollided;
        PlayerCollisionHandler.EnemyCollided += PlayerCollisionHandler_EnemyCollided;
        PlayerCollisionHandler.GroundCollided += PlayerCollisionHandler_GroundCollided;
        PlayerHealth.PlayerInvincibilityFinished += PlayerHealth_PlayerInvincibilityFinished;

    }

    private void OnDisable()
    {
        PlayerCollisionHandler.BonusCollided -= PlayerCollisionHandler_BonusCollided;
        PlayerCollisionHandler.EnemyCollided -= PlayerCollisionHandler_EnemyCollided;
        PlayerCollisionHandler.GroundCollided -= PlayerCollisionHandler_GroundCollided;
        PlayerHealth.PlayerInvincibilityFinished -= PlayerHealth_PlayerInvincibilityFinished;
    }

    private void Start()
    {
        _bonusPoints.Value = 0;
    }

    private void PlayerHealth_PlayerInvincibilityFinished()
    {
        _isDamaged = false;
    }

    /// <summary>
    /// Register a collision with bonus
    /// </summary>
    /// <param name="enemy">Codename for enemy to decide on a bonus value</param>
    /// <param name="collisionPosition">Position to spawn a UI element</param>
    private void PlayerCollisionHandler_BonusCollided(int enemy, Vector3 collisionPosition)
    {
        if (_isDamaged)
        {
            return;
        }
        _bonusRegisterd = true;
        _bonusEnemy = enemy;
        _bonusPosition = collisionPosition;
    }
    
    private void PlayerCollisionHandler_EnemyCollided()
    {
        _isDamaged = true;
        _bonusRegisterd = false;
    }
    
    /// <summary>
    /// Player needs to collide with the ground undamaged to confirm previously registered bonus
    /// </summary>
    private void PlayerCollisionHandler_GroundCollided()
    {
        if (!_bonusRegisterd)
        {
            return;
        }
        
        if (_bonusEnemy == 0)
        {
            _bonusPoints.Value += _kangarooBonus;
            _currentBonus = _kangarooBonus;
        }
        if (_bonusEnemy == 1)
        {
            _bonusPoints.Value += _birdBonus;
            _currentBonus = _birdBonus;
        }
        
        BonusUpdated?.Invoke(_currentBonus, _bonusPosition);
        _bonusRegisterd = false;
        
        // Check for heal
        if ((int)(_bonusPoints.Value / _healEveryNPoints) > _healCount)
        {
            HealBonusReached?.Invoke();
            _healCount++;
        }
    }
}
