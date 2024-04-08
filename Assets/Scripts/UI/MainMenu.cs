using System;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MainMenuBGTimelineControl _mainMenuBgTimelineControl;
    [SerializeField] private MainMenuButtonsTimelineControl _mainMenuButtonsTimelineControl;
    [SerializeField] private GameObject _enableDataCollectionButton;
    [SerializeField] private GameObject _disableDataCollectionButton;

    public void EnableDataCollectionSetting()
    {
        _enableDataCollectionButton.SetActive(false);
        _disableDataCollectionButton.SetActive(true);
        PlayerPrefs.SetInt("dataConsent", 1);
        UGSSetup.Instance.StartAnalyticsCollection();
    }
    
    public void DisableDataCollectionSetting()
    {
        _enableDataCollectionButton.SetActive(true);
        _disableDataCollectionButton.SetActive(false);
        PlayerPrefs.SetInt("dataConsent", 0);
        UGSSetup.Instance.StopAnalyticsCollection();
    }

    public void StartMainMenu()
    {
        _mainMenuBgTimelineControl.Play();
        _mainMenuButtonsTimelineControl.Setup();
        _mainMenuButtonsTimelineControl.Play();

        if(PlayerPrefs.GetInt("dataConsent") == 1)
        {
            _enableDataCollectionButton.SetActive(false);
            _disableDataCollectionButton.SetActive(true);
        }
        else
        {
            _enableDataCollectionButton.SetActive(true);
            _disableDataCollectionButton.SetActive(false);
        }
    }
}
