using System;
using UnityEngine;

public class MainSceneLoader : MonoBehaviour
{
    [SerializeField] private bool _isTestMode = false;
    [Header("Scene Preflight References")]
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _mainUi;
    [SerializeField] private GameObject _eventSystem;
    [Header("---")]
    [SerializeField] private Game _mainGame;
    

    private void OnEnable()
    {
        MainMenuLoader.OnNewGameSetUp += StartGame;
    }
    
    private void OnDisable()
    {
        MainMenuLoader.OnNewGameSetUp -= StartGame;
    }
    
    private void Awake()
    {
        _mainCamera.SetActive(false);
        _mainUi.SetActive(false);
        _eventSystem.SetActive(false);
    }
    
    private void StartGame()
    {
        _mainCamera.SetActive(true);
        _mainUi.SetActive(true);
        _eventSystem.SetActive(true);
        
        _mainGame.StartGame();
    }
    
    private void Start()
    {
        if (_isTestMode)
        {
            StartGame();
        }
    }
}
