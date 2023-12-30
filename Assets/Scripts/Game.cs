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
    [SerializeField] private StringVariable _currentUserName;
    // Enemies
    [SerializeField] private SpawnManager _spawnManager;
    
    // Global Game UI
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TextMeshProUGUI _userNameStats;
    
    // Game Over Setup
    [Header("-------------------------")]
    [Header("Game Over")]
    [SerializeField] private TextMeshProUGUI _gameOverStats;
    [SerializeField] private DeathScreenUiTimeineControl _deathScreenUiTimeineControl;
    [SerializeField] private DeathScreenButtonsUIControl _deathScreenButtonsUIControl;
    [SerializeField] private float _gameOverUIdelay;
    private WaitForSeconds _gameOverUIdelayWait;
 
    // Pause Handling
    public static List<IPausable> Pausables = new List<IPausable>();
    
    // Game Over event
    public static event Action OnGameOver;
    

    private bool _isGameOver = false;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += CheckLife;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= CheckLife;
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

    public static string GetDifficultyLevelName(int index)
    {
        switch (index)
        {
            case 0:
                return "Easy";
                break;
            case 1:
                return "Meduim";
                break;
            case 2:
                return "Hard";
                break;
        }
        return " ";
    }
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.P))
        {
            Debug.Break();              
            //PauseGame();
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
        _gameOverUIdelayWait = new WaitForSeconds(_gameOverUIdelay);
    }
    
    private void CheckLife()
    {
        // Game Over
        if (_playerHealth.Value == 0)
        {
            PauseGame();
            _isGameOver = true;
            StartCoroutine(GameOverScreenDelay());
            OnGameOver?.Invoke();
        }
    }

    private IEnumerator GameOverScreenDelay()
    {
        yield return _gameOverUIdelayWait;
        ShowGameOverUI();
    }
    private void ShowGameOverUI()
    {
        _gameOverUI.SetActive(true);
        _inGameUI.SetActive(false);
        _gameOverStats.text = ((int)_gameTime.Value).ToString() + " Seconds!!!";
        _userNameStats.text = _currentUserName.Value;
        _deathScreenUiTimeineControl.TimelinePlay();
        _deathScreenButtonsUIControl.TimelinePlay();
    }
}
