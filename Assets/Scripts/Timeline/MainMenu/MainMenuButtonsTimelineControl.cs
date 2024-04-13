using System;
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
    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_Text _userNameInputPlaceholderText;
    private int _difficulty = 0;
    private int _startScoreExitMode = 0;
    private string _username;
    private float _fps => (float)((TimelineAsset)_bgDirector.playableAsset).editorSettings.frameRate;

    public static event Action<int, string> OnGameStartAnimationOver;
    public static event Action OnStartButtonPressed;
    public static event Action OnShowScoresPressed;
    public static event Action OnExitGamePressed;
    
    public void Setup()
    {
        _username = CheckPlayerNamePref();
        _userNameInputPlaceholderText.text = _username;
        _userNameInput.text = _username;
        _bgDirector.time = 1f / _fps;
        _bgDirector.Evaluate();
    }
    
    public void Play()
    {
        _bgDirector.Play();
    }
    
    public void PerformMenuStartScoresExit()
    {
        if (_startScoreExitMode == 1)
        {
            OnShowScoresPressed?.Invoke();
        }
        else if (_startScoreExitMode == 2)
        {
            OnExitGamePressed?.Invoke();
        }
    }
    
    public void SetScoreScreenMode()
    {
        _startScoreExitMode = 1;
        _bgDirector.Pause();
        _bgDirector.time = _startGameStartTime / _fps;
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
    }

    public void StartGameTimeline()
    {
        _bgDirector.Pause();
        _bgDirector.time = _startGameStartTime / _fps;
        _bgDirector.Play();
        OnStartButtonPressed?.Invoke();
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
        _username = _userNameInput.text;
        if (_username == "")
        {
            _username = "Player1";
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", _username);
        }
        OnGameStartAnimationOver?.Invoke(_difficulty, _username);
    }
    
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
}
