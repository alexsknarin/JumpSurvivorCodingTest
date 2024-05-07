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
    // Ad integration
    [SerializeField] private AdManager _adManager;
    private bool _isAdOnStart = false;
    private bool _isAdOnEnd = false;
    

    private void OnEnable()
    {
        MainMenuLoader.OnNewGameSetUp += StartGame;
        DeathScreenButtonsUIControl.DeathUIButtonPressed += HandleDeathScreenButtonPress;
        _adManager.OnAdFinished += ContinueGameAfterAd;
    }
    
    private void OnDisable()
    {
        MainMenuLoader.OnNewGameSetUp -= StartGame;
        DeathScreenButtonsUIControl.DeathUIButtonPressed -= HandleDeathScreenButtonPress;
        _adManager.OnAdFinished -= ContinueGameAfterAd;
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

        // Check for Ad
        int currentAddIndex = PlayerPrefs.GetInt("currentAdIndex", 0);
        Debug.Log("currentAddIndex: " + currentAddIndex.ToString());
        if (currentAddIndex == 5)
        {
            _isAdOnStart = true;
            _adManager.Play();    
        }
        else
        {
            _mainGame.StartGame();
            PlayerPrefs.SetInt("currentAdIndex", currentAddIndex + 1);
        }
    }
    
    private void ContinueGameAfterAd()
    {
        if (_isAdOnStart)
        {
            _isAdOnStart = false;
            _mainGame.StartGame();
            PlayerPrefs.SetInt("currentAdIndex", 0);
        }
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
