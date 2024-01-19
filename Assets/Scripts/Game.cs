/* Current responsibilities of this class:
 * - Initialize/start a game.
 * - Stop the game based on condition this class is watching for.
 * - Pause/Unpause by a key press - TODO: remove it from the release version.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Initialisation of the all game systems, control of the flow of the game.
/// </summary>
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
    
    
    // Game Over Setup
    [Header("-------------------------")]
    [Header("Game Over")]
    [SerializeField] private float _gameOverUIdelay;
    [SerializeField] private DeathScreenUI _deathScreenUI;
    private WaitForSeconds _gameOverUIdelayWait;
    
    // UGS
    [Header("-------------------------")]
    [Header("Unity Game Services")]
    [SerializeField] private AnalyticsCollector _analyticsCollector;
    [SerializeField] private SubmitScoresToLeaderboard _submitScoresToLeaderboard;
 
    // Pause Handling
    public static List<IPausable> Pausables = new List<IPausable>();
    
    // Game Over event
    public static event Action GameOver;
    

    private bool _isGameOver = false;
    
    private void Start()
    {
        _spawnManager.InitSpawn();
        _gameOverUIdelayWait = new WaitForSeconds(_gameOverUIdelay);
        _deathScreenUI.gameObject.SetActive(false);
        
        // UGS
        // Call Analytics and Score Setups
        //_analyticsCollector.Setup();
        _submitScoresToLeaderboard.Setup();
    }

    private void OnEnable()
    {
        PlayerHealth.HealthDecreased += PlayerHealth_HealthDecreased;
        DeathScreenButtonsUIControl.DeathUIButtonPressed += DeathScreenButtonsUIControl_DeathUIButtonPressed;
    }

    private void OnDisable()
    {
        PlayerHealth.HealthDecreased -= PlayerHealth_HealthDecreased;
        DeathScreenButtonsUIControl.DeathUIButtonPressed -= DeathScreenButtonsUIControl_DeathUIButtonPressed;
    }
    
    /// <summary>
    /// This method is for debugging only. TODO: Remove 
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.P))
        {
            //Debug.Break();              
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
    private void PlayerHealth_HealthDecreased()
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

    /// <summary>
    /// Handle death screen button press
    /// </summary>
    /// <param name="sceneIndex">Scene to load</param>
    private void DeathScreenButtonsUIControl_DeathUIButtonPressed(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
