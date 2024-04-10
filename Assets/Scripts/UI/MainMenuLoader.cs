using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{   
    [Header("Scene Preflight References")]
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _mainUi;
    [Header("---")]
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private MainMenuDataManager _mainMenuDataManager;
    
    public static event Action OnNewGameSetUp;
    
    private void OnEnable()
    {
        GameLoader.OnLogoEnd += StartMainMenu;
        MainMenuButtonsTimelineControl.OnGameStartAnimationOver += StartMainScene;
        MainMenuButtonsTimelineControl.OnShowScoresPressed += ShowScores;
        MainMenuButtonsTimelineControl.OnExitGamePressed += ExitGame;
    }
    
    private void OnDisable()
    {
        GameLoader.OnLogoEnd -= StartMainMenu;
        MainMenuButtonsTimelineControl.OnGameStartAnimationOver -= StartMainScene;
        MainMenuButtonsTimelineControl.OnShowScoresPressed -= ShowScores;
        MainMenuButtonsTimelineControl.OnExitGamePressed -= ExitGame;
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
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            StartMainMenu();
        }
    }

    private void StartMainMenu()
    {
        _mainCamera.SetActive(true);
        _mainUi.SetActive(true);
        _mainMenu.StartMainMenu();
    }
    
    private void StartMainScene(int difficulty, string username)
    {
        Debug.Log(" ------------------------------------------- StartMainScene");
        _mainMenuDataManager.SaveGameData(difficulty, username);
        SceneManager.UnloadSceneAsync(1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        OnNewGameSetUp?.Invoke();
    }
    
    private void ShowScores()
    {
        SceneManager.LoadScene(3);
    }
    
    private void ExitGame()
    {
        _mainMenuDataManager.SaveGameData(0, "noname");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
