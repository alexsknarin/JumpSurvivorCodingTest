using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for lifebar and health icon management. Link to _bonusTextTransform is for hearts heal animation;
/// </summary>
public class PlayerHealthUIView : MonoBehaviour
{
    [SerializeField] private HealthHeartAnimation _healthIconPrefab;
    [SerializeField] private IntVariable _currentHealth;
    [SerializeField] private IntVariable _maxHealth;
    [SerializeField] private float _iconMargin;
    [SerializeField] private Transform _bonusTextTransform;
    private Vector3 _newPosition = Vector3.zero;
    private List<HealthHeartAnimation> _healthIcons = new List<HealthHeartAnimation>();
    [SerializeField] private float _nearDeathFraction;
    private float _nearDeathHealthNumber;

    private void OnEnable()
    {
        PlayerHealth.HealthDecreased += PlayerHealth_HealthDecreased;
        PlayerHealth.PlayerHealthSetUp += PlayerHealth_PlayerHealthSetUp;
        PlayerHealth.HealthIncreased += PlayerHealth_HealthIncreased;
    }

    private void OnDisable()
    {
        PlayerHealth.HealthDecreased -= PlayerHealth_HealthDecreased;
        PlayerHealth.PlayerHealthSetUp -= PlayerHealth_PlayerHealthSetUp;
        PlayerHealth.HealthIncreased -= PlayerHealth_HealthIncreased;
    }
    
    public void PlayerHealth_PlayerHealthSetUp()
    {
        // Generate Lifebar items
        for (int i = 0; i < _maxHealth.Value; i++)
        {
            _newPosition.x -= _iconMargin;
            HealthHeartAnimation newHealthHeartPrefab = Instantiate(_healthIconPrefab, Vector3.zero, _healthIconPrefab.transform.rotation, transform);
            newHealthHeartPrefab.transform.localPosition = _newPosition;
            newHealthHeartPrefab.SaveInitialState(_newPosition, transform.localPosition, _bonusTextTransform.localPosition);
            _healthIcons.Add(newHealthHeartPrefab);
        }

        _nearDeathHealthNumber = _maxHealth.Value * _nearDeathFraction;
    }

    private void PlayerHealth_HealthDecreased()
    {
        _healthIcons[_currentHealth.Value].StartDamageAnimation();
        CheckUpdateNearDeathState();
    }

    private void PlayerHealth_HealthIncreased()
    {
        _healthIcons[_currentHealth.Value-1].StartHealAnimation();
        CheckUpdateNearDeathState();
    }

    private void CheckUpdateNearDeathState()
    {
        if (_currentHealth.Value < _nearDeathHealthNumber)
        {
            for(int i=0; i<_currentHealth.Value; i++)
            {
                _healthIcons[i].EnableNearDeath();
            }    
        }
        else
        {
            for(int i=0; i<_currentHealth.Value; i++)
            {
                _healthIcons[i].DisableNearDeath();
            }    
        }
    }
}
