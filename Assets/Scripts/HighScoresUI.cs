using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class HighScoresUI : MonoBehaviour
{
    [SerializeField] private Button _backToMainMenuButton;

    void Start()
    {
        _backToMainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
