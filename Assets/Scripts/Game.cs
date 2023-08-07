using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Player
    [SerializeField] private IntVariable _playerHealth;
    
    // Enemies
    [SerializeField] private SpawnManager _spawnManager;
    
    // Global Game UI
    [SerializeField] private GameObject _inGamaUI;
    [SerializeField] private GameObject _gameOverUI;
 
    // Pause Handling
    public static List<IPausable> Pausables = new List<IPausable>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var p in Pausables)
            {
                p.SetPaused();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (var p in Pausables)
            {
                p.SetUnpaused();
            }
        }
    }

    private void Start()
    {
        _spawnManager.InitSpawn();
    }
    
    private void CheckLife()
    {
        if (_playerHealth.Value == 0)
        {
            _gameOverUI.SetActive(true);
        }
    }
}
