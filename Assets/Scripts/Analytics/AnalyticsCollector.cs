/* Single point of collection of all of the statistics for the Unity Analytics.
 * Player consent is handled separately, saved into PlayerPrefs and checked here
 * before starting gathering and sending data.
 * Currently there are only two custom event that are being watched:
 *  - playerDamaged with enemy type and spawnState.
 *  - playerDeath with spawnState.
 * To be able to analyze how often players are being damaged on each of the states of the game.
 */
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

/// <summary>
/// Class to collect all Analytics Data and send it to Unity Analytics service.
/// </summary>
public class AnalyticsCollector : MonoBehaviour
{
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private IntVariable _difficultyLevel;
    private CustomEvent _playerDamagedEvent = new CustomEvent("playerDamaged");
    private CustomEvent _playerDeathEvent = new CustomEvent("playerDeath");
    private string _spawnStateName = "";
    private int _spawnStateNum = 0;

    private void OnEnable()
    {
        PlayerCollisionHandler.AnalyticsEnemyCollided += PlayerCollisionHandler_AnalyticsEnemyCollided;
        SpawnManager.SpawnStateChanged += SpawnManager_SpawnStateChanged;
        Game.GameOver += Game_GameOver;
    }
    
    private void OnDisable()
    {
        PlayerCollisionHandler.AnalyticsEnemyCollided -= PlayerCollisionHandler_AnalyticsEnemyCollided;
        SpawnManager.SpawnStateChanged -= SpawnManager_SpawnStateChanged;
        Game.GameOver -= Game_GameOver;
    }
 
    private void SpawnManager_SpawnStateChanged(string spawnStateName)
    {
        _spawnStateName = spawnStateName;
        _spawnStateNum++;
    }

    private void PlayerCollisionHandler_AnalyticsEnemyCollided(EnemyTypes enemyType)
    {
        _playerDamagedEvent.Reset();
        _playerDamagedEvent.Add("difficultyLevel", _difficultyLevel.Value);
        _playerDamagedEvent.Add("enemyType", enemyType.ToString());
        _playerDamagedEvent.Add("spawnState", _spawnStateName);
        _playerDamagedEvent.Add("spawnStateNum", _spawnStateNum);
        AnalyticsService.Instance.RecordEvent(_playerDamagedEvent);
    }

    private void Game_GameOver()
    {
        _playerDeathEvent.Reset();
        _playerDeathEvent.Add("difficultyLevel", _difficultyLevel.Value);
        _playerDeathEvent.Add("spawnState", _spawnStateName);
        _playerDeathEvent.Add("spawnStateNum", _spawnStateNum);
        AnalyticsService.Instance.RecordEvent(_playerDeathEvent);
    }
}
