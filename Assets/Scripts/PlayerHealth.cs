using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private IntVariable[] _maxHealth;
    [SerializeField] private IntVariable _maxHealthCurrent;
    [SerializeField] private IntVariable _dificultyLevel;
    

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
    }

    private void DecreaseLives()
    {
        _playerHealth.Value -= 1;
    }
}