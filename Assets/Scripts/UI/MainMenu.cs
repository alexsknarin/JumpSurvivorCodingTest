using System;
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
        // check if data exists
        // if So skip consent
        // else:
        _consentScreen.SetActive(true);
    }

    private void StartMainMenu()
    {
        _consentScreen.SetActive(false);
        _mainMenuBgTimelineControl.Play();
        _mainMenuButtonsTimelineControl.Play();
    }

    public void AgreeToDataCollection()
    {
        // save data collection approval to the PlayerPrefs
        _enableDataCollectionButton.SetActive(false);
        _disableDataCollectionButton.SetActive(true);
        Debug.Log("Statistics collection Allowed - Saved");
        StartMainMenu();
    }
    
    public void RefuseDataCollection()
    {
        // save data collection disapproval to the PlayerPrefs
        _enableDataCollectionButton.SetActive(true);
        _disableDataCollectionButton.SetActive(false);
        Debug.Log("Statistics collection disallowed - Saved");
        StartMainMenu();
    }

    public void EnableDataCollectionSetting()
    {
        Debug.Log("ENABLE DATA");
    }
    
    public void DisableDataCollectionSetting()
    {
        Debug.Log("Disable DATA");
    }
}
