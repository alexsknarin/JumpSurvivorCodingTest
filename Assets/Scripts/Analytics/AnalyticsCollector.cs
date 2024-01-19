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

    public void Setup()
    {
        try
        {
            GiveConsent();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }
    
    private void SpawnManager_SpawnStateChanged(string spawnStateName)
    {
        _spawnStateName = spawnStateName;
        _spawnStateNum++;
    }

    private void GiveConsent()
    {
        if (PlayerPrefs.GetInt("dataConsent") == 1)
        {
            AnalyticsService.Instance.StartDataCollection();
            Debug.Log("Consent has been provided. The SDK is now collecting data");
        }
    }

    private void PlayerCollisionHandler_AnalyticsEnemyCollided(string enemyName)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"difficultyLevel", _difficultyLevel.Value},
            {"enemyType", enemyName},
            {"spawnState", _spawnStateName},
            {"spawnStateNum", _spawnStateNum}
        };
        
        AnalyticsService.Instance.CustomData("playerDamaged", parameters);
        
        AnalyticsService.Instance.Flush();
    }

    private void Game_GameOver()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"difficultyLevel", _difficultyLevel.Value},
            {"spawnState", _spawnStateName}
        };
        
        AnalyticsService.Instance.CustomData("playerDeath", parameters);
        
        AnalyticsService.Instance.Flush();
    }
}
