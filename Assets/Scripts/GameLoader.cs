using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private GameObject _consentScreen;
    [SerializeField] private Logo _logo;
    [SerializeField] private GameObject _mainUi;
    [SerializeField] private GameObject _mainCamera;

    public static bool GameLoaded { get; private set; }
    public static event Action OnLogoEnd;

    private void OnEnable()
    {
        _logo.OnLogoEnd += StartMainMenu;
        _logo.OnLogoFadeIn += StartMenuLoad;
    }
    
    private void OnDisable()
    {
        _logo.OnLogoEnd -= StartMainMenu;
        _logo.OnLogoFadeIn -= StartMenuLoad;
    }   

    private void Start()
    {
        // Application Settings
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        Application.targetFrameRate = 60;
        
        GameLoaded = false;
        if (PlayerPrefs.HasKey("dataConsent"))
        {
            StartLogo();
        }
        else
        {
            _consentScreen.SetActive(true);            
        }
    }
    public void AgreeToDataCollection()
    {
        PlayerPrefs.SetInt("dataConsent", 1);
        StartLogo();
    }
    
    public void RefuseDataCollection()
    {
        PlayerPrefs.SetInt("dataConsent", 0);
        StartLogo();
    }
    
    private void StartLogo()
    {
        _consentScreen.SetActive(false);
        _logo.gameObject.SetActive(true);
    }

    private void StartMenuLoad()
    {
        UGSSetup.Instance.Setup();
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        Debug.Log("StartMenuLoad");
    }

    private void StartMainMenu()
    {
        _logo.gameObject.SetActive(false);
        GameLoaded = true;
        OnLogoEnd?.Invoke();
        SceneManager.UnloadSceneAsync(0);
    }
}
