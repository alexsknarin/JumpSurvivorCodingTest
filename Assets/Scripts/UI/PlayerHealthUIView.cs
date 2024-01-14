using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for lifebar and health icon management. Link to _bonusTextTransform is for hearts heal animation;
/// </summary>
public class PlayerHealthUIView : MonoBehaviour
{
    [SerializeField] private HealthHeartAnimation _healthHeartPrefab;
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private IntVariable _maxHealth;
    [SerializeField] private float _heartsDistance;
    [SerializeField] private Transform _bonusTextTransform;
    private Vector3 _newPosition = Vector3.zero;
    private List<HealthHeartAnimation> _healthHearts = new List<HealthHeartAnimation>();
    [SerializeField] private float _nearDeathFraction;
    private float _nearDeathHealthNumber;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += ReceiveDamage;
        PlayerHealth.OnPlayerHealthSetUp += Initialize;
        PlayerHealth.OnHealthIncreased += HealDamage;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= ReceiveDamage;
        PlayerHealth.OnPlayerHealthSetUp -= Initialize;
        PlayerHealth.OnHealthIncreased -= HealDamage;
    }
    
    public void Initialize()
    {
        // Generate Lifebar items
        for (int i = 0; i < _maxHealth.Value; i++)
        {
            _newPosition.x -= _heartsDistance;
            HealthHeartAnimation newHealthHeartPrefab = Instantiate(_healthHeartPrefab, Vector3.zero, _healthHeartPrefab.transform.rotation, transform);
            newHealthHeartPrefab.transform.localPosition = _newPosition;
            newHealthHeartPrefab.SaveInitialState(_newPosition, transform.localPosition, _bonusTextTransform.localPosition);
            _healthHearts.Add(newHealthHeartPrefab);
        }

        _nearDeathHealthNumber = _maxHealth.Value * _nearDeathFraction;
    }

    private void ReceiveDamage()
    {
        _healthHearts[_playerHealth.Value].DoDamage();
        CheckUpdateNearDeathState();
    }

    private void HealDamage()
    {
        _healthHearts[_playerHealth.Value-1].DoHeal();
        CheckUpdateNearDeathState();
    }

    private void CheckUpdateNearDeathState()
    {
        if (_playerHealth.Value < _nearDeathHealthNumber)
        {
            for(int i=0; i<_playerHealth.Value; i++)
            {
                _healthHearts[i].EnableNearDeath();
            }    
        }
        else
        {
            for(int i=0; i<_playerHealth.Value; i++)
            {
                _healthHearts[i].DisableNearDeath();
            }    
        }
    }
}
