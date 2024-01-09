using System;
using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;

/// <summary>
/// Serialize current score as JSON and save it to Unity Cloud.
/// </summary>
public class SaveScoresToCloud : MonoBehaviour
{
    [SerializeField] private StringVariable _playerName;
    [SerializeField] private IntVariable _difficultyLevel;
    [SerializeField] private FloatVariable _gameTime;
    private string _dificultyLevelString;

    private void Start()
    {
        _dificultyLevelString = _difficultyLevel.Value switch
        {
            0 => "Easy",
            1 => "Medium",
            2 => "Hard",
            _ => _dificultyLevelString
        };
    }

    private void OnEnable()
    {
        Game.OnGameOver += SaveSoreData;
    }

    private void OnDisable()
    {
        Game.OnGameOver -= SaveSoreData;
    }
    
    private async void SaveSoreData()
    {
        var saveData = new Dictionary<string, object>
        {
            { "playerName", _playerName.Value},
            { "scores", _gameTime.Value.ToString() },
            { "difficultyLevel", _dificultyLevelString }
        };
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
    }
}
