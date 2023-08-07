using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private IntVariable _maxHealth;
    

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
        _playerHealth.Value = _maxHealth.Value;
    }

    private void DecreaseLives()
    {
        _playerHealth.Value -= 1;
    }
}