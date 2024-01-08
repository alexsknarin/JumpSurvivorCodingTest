using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

/// <summary>
/// Control animation of main Menu screen Control Buttons using Timeline.
/// </summary>
public class MainMenuButtonsTimelineControl : MonoBehaviour
{
    [SerializeField] private PlayableDirector _bgDirector;
    [SerializeField] private float _startGameStartTime;
    [SerializeField] private float _playerNameSetStartTime;
    [SerializeField] private float _dificultyLevelSetStartTime;
    [SerializeField] private IntVariable _difficultyLevelVariable;
    [SerializeField] private StringVariable _userNameVariable;
    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_Text _userNameInputPlaceholderText;
    private int _difficulty = 0;
    private int _startScoreExitMode = 0;
    private float _fps => (float)((TimelineAsset)_bgDirector.playableAsset).editorSettings.frameRate;


    private string CheckPlayerNamePref()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            return PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            return "Player1";
        }
    }
    
    public void Setup()
    {
        _userNameInputPlaceholderText.text = CheckPlayerNamePref();
    }
    
    public void SetScoreScreenMode()
    {
        _startScoreExitMode = 1;
        _bgDirector.Pause();
        _bgDirector.time = _startGameStartTime / _fps;
        _bgDirector.Play();
    }
    
    public void Play()
    {
        _bgDirector.Play();
    }

    public void SetExitGameMode()
    {
        _startScoreExitMode = 2;
        _bgDirector.Pause();
        _bgDirector.time = _startGameStartTime / _fps;
        _bgDirector.Play();
    }
    
    public void PauseTimeline()
    {
        _bgDirector.Pause();
        Debug.Log("Pause");
    }

    public void StartGameTimeline()
    {
        _bgDirector.Pause();
        _bgDirector.time = _startGameStartTime / _fps;
        _bgDirector.Play();
    }
    
    public void SetPlayerNameTimeline()
    {
        _bgDirector.Pause();
        _bgDirector.time = _playerNameSetStartTime / _fps;
        _bgDirector.Play();
    }
    
    public void SetDificultyLevelTimeline(int difficulty)
    {
        _bgDirector.Pause();
        _bgDirector.time = _dificultyLevelSetStartTime / _fps;
        _difficulty = difficulty;
        _bgDirector.Play();
    }
    
    public void StartGame()
    {
        string userName = _userNameInput.text;
        if (userName == "")
        {
            userName = "Player1";
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", userName);
        }
        _userNameVariable.Value = userName;
        _difficultyLevelVariable.Value = _difficulty;
        SceneManager.LoadScene(1);
    }

    public void MenuStartScoresExit()
    {
        if (_startScoreExitMode == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if (_startScoreExitMode == 2)
        {
            ExitGame();
        }
    }
    
    private void ExitGame()
    {
        
        _difficultyLevelVariable.Value = 0;
        _userNameVariable.Value = "noname";
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
