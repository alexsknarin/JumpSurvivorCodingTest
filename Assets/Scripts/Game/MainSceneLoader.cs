using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    [SerializeField] private bool _isTestMode = false;
    [SerializeField] private IntVariable _restartMode;
    [Header("Scene Preflight References")]
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _mainUi;
    [Header("---")]
    [SerializeField] private Game _mainGame;
    

    private void OnEnable()
    {
        MainMenuLoader.OnNewGameSetUp += StartGame;
        DeathScreenButtonsUIControl.DeathUIButtonPressed += HandleDeathScreenButtonPress;
    }
    
    private void OnDisable()
    {
        MainMenuLoader.OnNewGameSetUp -= StartGame;
        DeathScreenButtonsUIControl.DeathUIButtonPressed -= HandleDeathScreenButtonPress;
    }
    
    private void Awake()
    {
        _mainCamera.SetActive(false);
        _mainUi.SetActive(false);
    }
    
    private void StartGame()
    {
        _mainCamera.SetActive(true);
        _mainUi.SetActive(true);
        _mainGame.StartGame();
    }

    private void Start()
    {
        if (_isTestMode || _restartMode.Value == 1)
        {
            _restartMode.Value = 0;
            StartGame();
        }
    }
    
    /// <summary>
    /// Handle death screen button press
    /// </summary>
    /// <param name="sceneIndex">Scene to load</param>
    private void HandleDeathScreenButtonPress(int sceneIndex)
    {
        // Loading the same scene
        if(sceneIndex == 2)
        {
            _restartMode.Value = 1;
        }
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
