using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{   
    [Header("Scene Preflight References")]
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _mainUi;
    [SerializeField] private GameObject _eventSystem;
    [Header("---")]
    [SerializeField] private MainMenu _mainMenu;
    
    public static event Action OnNewGameSetUp;
    
    private void OnEnable()
    {
        GameLoader.OnLogoEnd += StartMainMenu;
        MainMenuButtonsTimelineControl.OnStartButtonPressed += LoadMainScene;
        MainMenuButtonsTimelineControl.OnGameStartAnimationOver += StartMainScene;
    }
    
    private void OnDisable()
    {
        GameLoader.OnLogoEnd -= StartMainMenu;
        MainMenuButtonsTimelineControl.OnStartButtonPressed -= LoadMainScene;
        MainMenuButtonsTimelineControl.OnGameStartAnimationOver -= StartMainScene;
    }
    
    private void Awake()
    {
        // Set Main Menu animation to the first frame of the animation
        _mainMenu.Setup();
    }
    
    private void Start()
    {
        if (GameLoader.GameLoaded)
        {
            Debug.Log("MainMenuLoader Start");
            StartMainMenu();
        }
    }

    private void StartMainMenu()
    {
        _mainCamera.SetActive(true);
        _mainUi.SetActive(true);
        _eventSystem.SetActive(true);
        _mainMenu.StartMainMenu();
        
        // SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

    private void LoadMainScene()
    {
    }
    
    private void StartMainScene()
    {
        _eventSystem.SetActive(false);
        SceneManager.UnloadSceneAsync(1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        OnNewGameSetUp?.Invoke();
    }
}
