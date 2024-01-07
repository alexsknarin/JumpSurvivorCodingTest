using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthUIView : MonoBehaviour
{
    [SerializeField] private HealthHeartAnimation _healthHeartPrefab;
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private IntVariable _maxHealth;
    [SerializeField] private float _heartsDistance;
    private Vector3 _newPosition = Vector3.zero;
    private List<HealthHeartAnimation> _healthHearts = new List<HealthHeartAnimation>();
    private bool _nearDeath = false;
    [SerializeField] private float _nearDeathFraction;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += RecieveDamage;
        PlayerHealth.OnPlayerHealthSetUp += Initialize;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= RecieveDamage;
        PlayerHealth.OnPlayerHealthSetUp -= Initialize;
    }
    
    public void Initialize()
    {
        // Generate Lifebar items
        for (int i = 0; i < _maxHealth.Value; i++)
        {
            _newPosition.x -= _heartsDistance;
            HealthHeartAnimation newHealthHeartPrefab = Instantiate(_healthHeartPrefab, Vector3.zero, _healthHeartPrefab.transform.rotation, transform);
            newHealthHeartPrefab.transform.localPosition = _newPosition;
            _healthHearts.Add(newHealthHeartPrefab);
        }
    }

    private void RecieveDamage()
    {
        if (_playerHealth.Value < _maxHealth.Value * _nearDeathFraction)
        {
            if (!_nearDeath)
            {
                for(int i=0; i<_playerHealth.Value; i++)
                {
                    _healthHearts[i].DoNearDeath();
                }    
            }
            _nearDeath = true;
        }
        _healthHearts[_playerHealth.Value].DoDamage();
    }
}
