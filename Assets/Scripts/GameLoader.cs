using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private GameObject _consentScreen;
    [SerializeField] private Logo _logo;
    [SerializeField] private GameObject _eventSystem;
    public static bool GameLoaded { get; private set; }
    public static event Action OnLogoEnd;

    private void OnEnable()
    {
        _logo.OnLogoEnd += StartMainMenu;
    }
    
    private void OnDisable()
    {
        _logo.OnLogoEnd -= StartMainMenu;
    }   

    private void Awake()
    {
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
        UGSSetup.Instance.Setup();
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        _logo.gameObject.SetActive(true);
    }

    private void StartMainMenu()
    {
        _eventSystem.SetActive(false);
        _logo.gameObject.SetActive(false);
        GameLoaded = true;
        SceneManager.UnloadSceneAsync(0);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        OnLogoEnd?.Invoke();
    }
}
