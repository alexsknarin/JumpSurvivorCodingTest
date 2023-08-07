using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // Player
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private FloatVariable _gameTime;
    // Enemies
    [SerializeField] private SpawnManager _spawnManager;
    
    // Global Game UI
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TextMeshProUGUI _gameOverStats;
    [SerializeField] private StringVariable _currentUserName;
 
    // Pause Handling
    public static List<IPausable> Pausables = new List<IPausable>();

    private bool _isGameOver = false;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += CheckLife;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= CheckLife;
    }
    
    private void PauseGame()
    {
        foreach (var p in Pausables)
        {
            p.SetPaused();
        }
    }
    
    private void UnPauseGame()
    {
        foreach (var p in Pausables)
        {
            p.SetUnpaused();
        }
    }
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            UnPauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver)
        {
            SceneManager.LoadScene(0);
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
            _inGameUI.SetActive(false);
            _gameOverStats.text = "Player Name: " + _currentUserName.Value + "\n" + "Time: " + ((int)_gameTime.Value).ToString() + " seconds";
            PauseGame();
            _isGameOver = true;
        }
    }
}
