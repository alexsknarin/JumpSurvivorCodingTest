using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    // UI
    [SerializeField] private GameObject _inGamaUI;
    [SerializeField] private GameObject _gameOverUI;
    
    [SerializeField] private IntVariable _playerHealth;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += CheckLife;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= CheckLife;
    }
    
    private void Start()
    {
    }

    private void CheckLife()
    {
        if (_playerHealth.Value == 0)
        {
            Time.timeScale = 0;
            _gameOverUI.SetActive(true);
        }
    }
}
