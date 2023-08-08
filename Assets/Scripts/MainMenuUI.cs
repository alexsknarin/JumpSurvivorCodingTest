using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_Dropdown _difficutlyLevelDropdown;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _hightScoreButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private IntVariable _difficultyLevelVariable;
    [SerializeField] private StringVariable _userNameVariable;

    private void Start()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _exitGameButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        string userName = _userNameInput.text;
        if (userName == "")
        {
            userName = "noname";
        }
        _userNameVariable.Value = userName;
        _difficultyLevelVariable.Value = _difficutlyLevelDropdown.value;
        SceneManager.LoadScene(0);
    }

    private void ExitGame()
    {
        Application.Quit();
        _difficultyLevelVariable.Value = 0;
        _userNameVariable.Value = "noname";
    }
}
