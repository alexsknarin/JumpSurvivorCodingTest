/* This class is to initialize connection to Unity Game Services 
 * Used by LeaderBoards, Analytics an CloudSave
 * Implemented as a singleton because I need to be sure that I connect to ugs only once
 * per game session.
 */

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class UGSSetup : MonoBehaviour
{
    public static UGSSetup Instance { get; private set; }
    
    private bool _isConnecceted = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
    public async void Setup()
    {
        if (!_isConnecceted)
        {
            
            Debug.Log("Setting up UGS");
            var options = new InitializationOptions();
            options.SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            SignInAnonymously();
            GiveConsent();
            _isConnecceted = true;
        }
    }
    
    private async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            Debug.Log("Authentification Failed");
            Debug.Log(s);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    
    private void GiveConsent()
    {
        if (PlayerPrefs.GetInt("dataConsent") == 1)
        {
            AnalyticsService.Instance.StartDataCollection();
            Debug.Log("Consent has been provided. The SDK is now collecting data");
        }
    }
    
    public void StopAnalyticsCollection()
    {
        AnalyticsService.Instance.StopDataCollection();
    }

    public void StartAnalyticsCollection()
    {
        AnalyticsService.Instance.StartDataCollection();
    }
}
