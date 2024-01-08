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

    [SerializeField] private DisplayHighScore _displayHighScoreEasy;
    [SerializeField] private DisplayHighScore _displayHighScoreMedium;
    [SerializeField] private DisplayHighScore _displayHighScoreHard;

    private async void Awake()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName("production");
        await UnityServices.InitializeAsync(options);
    }
    
    private async void Start()
    {
        _backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        _showLocalScoresButton.onClick.AddListener(ShowLocalScores);
        _showOnlineScoresButton.onClick.AddListener(ShowOnlineScores);
        ShowLocalScores();
        SignInAnonymously();
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

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
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
}
