using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _consentScreen;
    [SerializeField] private MainMenuBGTimelineControl _mainMenuBgTimelineControl;
    [SerializeField] private MainMenuButtonsTimelineControl _mainMenuButtonsTimelineControl;
    [SerializeField] private GameObject _enableDataCollectionButton;
    [SerializeField] private GameObject _disableDataCollectionButton;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("dataConsent") == 1)
        {
            AgreeToDataCollection();
        }
        else
        {
            _consentScreen.SetActive(true);            
        }
    }
    
    public void AgreeToDataCollection()
    {
        _enableDataCollectionButton.SetActive(false);
        _disableDataCollectionButton.SetActive(true);
        StartMainMenu();
        PlayerPrefs.SetInt("dataConsent", 1);
    }
    
    public void RefuseDataCollection()
    {
        _enableDataCollectionButton.SetActive(true);
        _disableDataCollectionButton.SetActive(false);
        StartMainMenu();
        PlayerPrefs.SetInt("dataConsent", 0);
    }

    public void EnableDataCollectionSetting()
    {
        PlayerPrefs.SetInt("dataConsent", 1);
    }
    
    public void DisableDataCollectionSetting()
    {
        PlayerPrefs.SetInt("dataConsent", 0);
    }

    private void StartMainMenu()
    {
        _consentScreen.SetActive(false);
        _mainMenuBgTimelineControl.Play();
        _mainMenuButtonsTimelineControl.Setup();
        _mainMenuButtonsTimelineControl.Play();
    }


}
