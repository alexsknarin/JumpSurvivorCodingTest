using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;


public class AnalyticsCollector : MonoBehaviour
{
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private IntVariable _difficultyLevel;
    private string _spawnStateName = "";
    private int _spawnStateNum = 0;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnAnalyticsEnemyCollided += PlayerDamagedCustomEvent;
        SpawnManager.OnSpawnStateChanged += SpawnStateNameChange;
        Game.OnGameOver += PlayerDeathCustomEvent;
    }
    
    private void OnDisable()
    {
        PlayerCollisionHandler.OnAnalyticsEnemyCollided -= PlayerDamagedCustomEvent;
        SpawnManager.OnSpawnStateChanged -= SpawnStateNameChange;
        Game.OnGameOver -= PlayerDeathCustomEvent;
    }

    private void SpawnStateNameChange(string spawnStateName)
    {
        _spawnStateName = spawnStateName;
        _spawnStateNum++;
    }

    async void Start()
    {
        try
        {
            var options = new InitializationOptions();
            options.SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            // await UnityServices.InitializeAsync();
            GiveConsent();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void GiveConsent()
    {
        if (PlayerPrefs.GetInt("dataConsent") == 1)
        {
            AnalyticsService.Instance.StartDataCollection();
            Debug.Log("Consent has been provided. The SDK is now collecting data");
        }
    }

    private void PlayerDamagedCustomEvent(string enemyName)
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

    private void PlayerDeathCustomEvent()
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
