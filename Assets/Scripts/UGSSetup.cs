using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class UGSSetup : MonoBehaviour
{
    [SerializeField] private AnalyticsCollector _analyticsCollector;
    [SerializeField] private SubmitScoresToLeaderboard _submitScoresToLeaderboard;

    async void Awake()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName("production");
        await UnityServices.InitializeAsync(options);
        
        // Call Analytics and Score Setups
        _analyticsCollector.Setup();
        _submitScoresToLeaderboard.Setup();
    }
}
