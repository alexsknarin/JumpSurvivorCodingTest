using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtonsUI : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _highScoresButton;
    [SerializeField] private Button _MainMenuButton;
    
    void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _highScoresButton.onClick.AddListener(ShowHighScores);
        _MainMenuButton.onClick.AddListener(ShowMainMenu);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ShowHighScores()
    {
        SceneManager.LoadScene(2);
    }

    private void ShowMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
