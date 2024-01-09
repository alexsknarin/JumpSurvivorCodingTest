using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;

public class SubmitScoresToLeaderboard : MonoBehaviour
{
    [SerializeField] private StringVariable _playerName;
    [SerializeField] private IntVariable _difficultyLevel;
    [SerializeField] private FloatVariable _gameTime;

    private const string _leaderboardIDEasy = "paw_easy01";
    private const string _leaderboardIDMedium = "paw_medium01";
    private const string _leaderboardIDHard = "paw_hard01";
    private string _leaderboardIDCurrent;

    private void OnEnable()
    {
        Game.OnGameOver += AddScore;
    }

    private void OnDisable()
    {
        Game.OnGameOver -= AddScore;
    }
    
    public void Setup()
    {
        switch (_difficultyLevel.Value)
        {
            case 0:
                _leaderboardIDCurrent = _leaderboardIDEasy;
                break;
            case 1:
                _leaderboardIDCurrent = _leaderboardIDMedium;
                break;
            case 2:
                _leaderboardIDCurrent = _leaderboardIDHard;
                break;
        }
    }

    public async void AddScore()
    {
        var metadata = new Dictionary<string, string>()
        {
            {"Nickname", _playerName.Value}  
        };
        try
        {
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(
                _leaderboardIDCurrent,
                _gameTime.Value,
                new AddPlayerScoreOptions { Metadata = metadata }
            );
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to submit scores: {e}");
        }
    }
}
