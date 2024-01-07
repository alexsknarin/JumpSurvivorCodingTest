using System;
using Unity.VisualScripting;
using UnityEngine;

public class BonusPointsManager : MonoBehaviour
{
    [SerializeField] private IntVariable _bonusPoints;
    [SerializeField] private int _kangarooBonus;
    private bool _bonusRegisterd = false;
    private int _bonusEnemy;
    private Vector3 _bonusPosition;
    private bool _isDamaged = false;

    public delegate void BonusUpdated(int enemy, Vector3 playerPosition);
    public static event BonusUpdated OnBonusUpdated;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnBonusCollided += RegisterBonus;
        PlayerCollisionHandler.OnEnemyCollided += DiscardBonus;
        PlayerCollisionHandler.OnGroundCollided += ApplyBonusPoints;

    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnBonusCollided -= RegisterBonus;
        PlayerCollisionHandler.OnEnemyCollided -= DiscardBonus;
        PlayerCollisionHandler.OnGroundCollided -= ApplyBonusPoints;
    }

    private void Start()
    {
        _bonusPoints.Value = 0;
    }

    private void RegisterBonus(int enemy, Vector3 collisionPosition)
    {
        _bonusRegisterd = true;
        _bonusEnemy = enemy;
        _bonusPosition = collisionPosition;
    }

    private void DiscardBonus()
    {
        _isDamaged = true;
        _bonusRegisterd = false;
    }
    
    private void ApplyBonusPoints()
    {
        if (!_bonusRegisterd)
        {
            return;
        }
        
        if (_bonusEnemy == 0)
        {
            _bonusPoints.Value += _kangarooBonus;
            OnBonusUpdated?.Invoke(_bonusEnemy, _bonusPosition);
            _bonusRegisterd = false;
        }
    }
}
