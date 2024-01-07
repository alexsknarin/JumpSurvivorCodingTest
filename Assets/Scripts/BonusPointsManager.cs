using UnityEngine;

public class BonusPointsManager : MonoBehaviour
{
    [SerializeField] private IntVariable _bonusPoints;
    [SerializeField] private int _kangarooBonus;
    [SerializeField] private int _birdBonus;
    private bool _bonusRegisterd = false;
    private int _bonusEnemy;
    private Vector3 _bonusPosition;
    private bool _isDamaged = false;
    private int _currentBonus;

    public delegate void BonusUpdated(int enemy, Vector3 playerPosition);
    public static event BonusUpdated OnBonusUpdated;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnBonusCollided += RegisterBonus;
        PlayerCollisionHandler.OnEnemyCollided += DiscardBonus;
        PlayerCollisionHandler.OnGroundCollided += ApplyBonusPoints;
        PlayerHealth.OnPlayerInvincibilityFinished += SetUndamaged;

    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnBonusCollided -= RegisterBonus;
        PlayerCollisionHandler.OnEnemyCollided -= DiscardBonus;
        PlayerCollisionHandler.OnGroundCollided -= ApplyBonusPoints;
        PlayerHealth.OnPlayerInvincibilityFinished -= SetUndamaged;
    }

    private void Start()
    {
        _bonusPoints.Value = 0;
    }

    private void SetUndamaged()
    {
        _isDamaged = false;
    }

    private void RegisterBonus(int enemy, Vector3 collisionPosition)
    {
        if (_isDamaged)
        {
            return;
        }
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
            _currentBonus = _kangarooBonus;
        }
        if (_bonusEnemy == 1)
        {
            _bonusPoints.Value += _birdBonus;
            _currentBonus = _birdBonus;
        }
        
        OnBonusUpdated?.Invoke(_currentBonus, _bonusPosition);
        _bonusRegisterd = false;
    }
}
