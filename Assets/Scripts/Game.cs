/* Current responsibilities of this class:
 * - Initialize/start a game.
 * - Stop the game based on condition this class is watching for.
 * - Pause/Unpause by a key press - TODO: remove it from the release version.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Initialisation of the all game systems, control of the flow of the game.
/// </summary>
public class Game : MonoBehaviour
{
    // Player
    [SerializeField] private Player _player;
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private StringVariable _currentUserName;
    // Enemies
    [SerializeField] private SpawnManager _spawnManager;

    // Global Game UI
    [Header("-------------------------")]
    [Header("Global UI")]
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _gameOverUI;
    
    [Header("-------------------------")]
    [Header("Tutorial")]
    [SerializeField] private TutorialManager _tutorialManager;

    // Game Over Setup
    [Header("-------------------------")]
    [Header("Game Over")]
    [SerializeField] private float _gameOverUIdelay;
    [SerializeField] private DeathScreenUI _deathScreenUI;
    private WaitForSeconds _gameOverUIdelayWait;
    
    // UGS
    [Header("-------------------------")]
    [Header("Unity Game Services")]
    [SerializeField] private UGSSetup _ugsSetup;
    [SerializeField] private SubmitScoresToLeaderboard _submitScoresToLeaderboard;
    
    // Scene Preflight
    [Header("-------------------------")]
    [Header("Scene Preflight")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _mainCanvas;
 
    // Pause Handling
    public static List<IPausable> Pausables = new List<IPausable>();
    
    // Game Over event
    public static event Action GameOver;
    private bool _isGameOver = false;

    public void StartGame()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        // UGS
        // Call Analytics and Score Setups
        UGSSetup.Instance.Setup();    
        _submitScoresToLeaderboard.Setup();
        
        _player.Initialize();   
        _spawnManager.InitSpawn();
        _gameOverUIdelayWait = new WaitForSeconds(_gameOverUIdelay);
        _deathScreenUI.gameObject.SetActive(false);
        _tutorialManager.Play();
    }
    
    private void OnEnable()
    {
        PlayerHealth.HealthDecreased += HandleDecreaseHealth;
        
    }

    private void OnDisable()
    {
        PlayerHealth.HealthDecreased -= HandleDecreaseHealth;
        
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
    
    /// <summary>
    /// Each Time helath decreased this method check if it was the last health and it is time for GameOver event
    /// </summary>
    private void HandleDecreaseHealth()
    {
        // Game Over
        if (_playerHealth.Value == 0)
        {
            PauseGame();
            _isGameOver = true;
            StartCoroutine(GameOverScreenDelay());
            GameOver?.Invoke();
        }
    }

    private IEnumerator GameOverScreenDelay()
    {
        yield return _gameOverUIdelayWait;
        ShowGameOverUI();
    }
    private void ShowGameOverUI()
    {
        //_inGameUI.SetActive(false);
        _deathScreenUI.Play(((int)_gameTime.Value).ToString() + " Seconds!!!", _currentUserName.Value);
    }
}
