using System;
using System.IO;
using TMPro;
using Unity.Services.Leaderboards;
using UnityEngine;


/// <summary>
/// Component to Handle formatting and displaying high score data on a screen.
/// </summary>
public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private string _difficultyLevel;
    [SerializeField] private string _leaderboardID; // TODO: get leaderboard 
    private int _textWidth = 35;

    public void ShowLocalScores()
    {
        // Read Json file
        string fullFileName = Application.persistentDataPath + "/SaveData/" + _difficultyLevel + "_saveSoreFile.json";
        if (!File.Exists(fullFileName))
        {
            return;
        }

        string json = File.ReadAllText(fullFileName);
        SaveContainer saveData = JsonUtility.FromJson<SaveContainer>(json);

        string text = _difficultyLevel + ":\n";
        foreach (var entry in saveData.Entries)
        {
            int padding = _textWidth - (entry.Name.Length + entry.Time.ToString().Length + 2);   
            text += entry.Name + " " + "-".PadLeft(padding, '-') + " " + entry.Time + "\n";
        }

        _scoreText.text = text;
    }

    public async void ShowOnlineScores()
    {
        _scoreText.text = "";
        
        string text = _difficultyLevel + ":\n";
        try
        {
            var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(_leaderboardID, new GetScoresOptions {IncludeMetadata = true});
            foreach (var score in scoreResponse.Results)
            {
                LeaderboardMetadata data = JsonUtility.FromJson<LeaderboardMetadata>(score.Metadata);
                text += data.Nickname + " --- " + ((int)score.Score).ToString() + "\n";
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            text += "No Data Available";
        }
        
        _scoreText.text = text;
    }
}
