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
    
    private void OnEnable()
    {
        GameLoader.OnLogoEnd += StartMainMenu;
        MainMenuButtonsTimelineControl.OnStartButtonPressed += LoadGame;
    }
    
    private void OnDisable()
    {
        GameLoader.OnLogoEnd -= StartMainMenu;
        MainMenuButtonsTimelineControl.OnStartButtonPressed -= LoadGame;
    }
    
    private void Awake()
    {
        _mainCamera.SetActive(false);
        _mainUi.SetActive(false);
        _eventSystem.SetActive(false);
        
        if (GameLoader.GameLoaded)
        {
            StartMainMenu();
        }
    }
    
    private void StartMainMenu()
    {
        _mainCamera.SetActive(true);
        _mainUi.SetActive(true);
        _eventSystem.SetActive(true);
        _mainMenu.StartMainMenu();
    }

    private void LoadGame()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);   
    }
    
    private void StartGame()
    {
        
    }
}
