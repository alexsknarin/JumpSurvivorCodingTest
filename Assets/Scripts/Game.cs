using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Life _life;
    // UI
    [SerializeField] private GameObject _inGamaUI;
    [SerializeField] private GameObject _gameOverUI;

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
        _life = _player.GetComponent<Life>();
    }

    private void CheckLife()
    {
        if (_life.Lives == 0)
        {
            Time.timeScale = 0;
            _gameOverUI.SetActive(true);
        }
    }
}
