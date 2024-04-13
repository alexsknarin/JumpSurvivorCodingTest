using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine.UI;

public class HighScoresUI : MonoBehaviour
{
    [SerializeField] private Button _backToMainMenuButton;
    [SerializeField] private Button _showLocalScoresButton;
    [SerializeField] private Button _showOnlineScoresButton;
    [SerializeField] private Button _showOnlineDataButton;

    [SerializeField] private DisplayHighScore _displayHighScoreEasy;
    [SerializeField] private DisplayHighScore _displayHighScoreMedium;
    [SerializeField] private DisplayHighScore _displayHighScoreHard;

    private async void Awake()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName("production");
        await UnityServices.InitializeAsync(options);
    }
    
    private void Start()
    {
        _backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        _showLocalScoresButton.onClick.AddListener(ShowLocalScores);
        _showOnlineScoresButton.onClick.AddListener(ShowOnlineScores);
        _showOnlineDataButton.onClick.AddListener(ShowOnlineData);
        ShowLocalScores();
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void ShowLocalScores()
    {
        _displayHighScoreEasy.ShowLocalScores();
        _displayHighScoreMedium.ShowLocalScores();
        _displayHighScoreHard.ShowLocalScores();
    }
    
    private void ShowOnlineScores()
    {
        _displayHighScoreEasy.ShowOnlineScores();
        _displayHighScoreMedium.ShowOnlineScores();
        _displayHighScoreHard.ShowOnlineScores();
    }
    
    private void ShowOnlineData()
    {
        _displayHighScoreEasy.Clear();
        _displayHighScoreMedium.ShowOnlineData();
        _displayHighScoreHard.Clear();
    }
    
}
