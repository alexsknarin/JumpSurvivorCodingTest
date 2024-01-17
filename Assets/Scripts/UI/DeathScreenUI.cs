using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DeathScreenUI : MonoBehaviour
{
    [Header("Death Screen Timelines")]
    [SerializeField] private PlayableDirector _bgDirector;
    [SerializeField] private DeathScreenButtonsUIControl _deathScreenButtonsUIControl;
    [SerializeField] private TextMeshProUGUI _userNameStats;
    [SerializeField] private TextMeshProUGUI _secondsStats;
    [SerializeField] private int _loopBGFromFrame;
    [SerializeField] private GameObject _buttons;
    private float _fps => (float)((TimelineAsset)_bgDirector.playableAsset).editorSettings.frameRate;

    private void Start()
    {
        _buttons.SetActive(false);
    }

    private void OnEnable()
    {
        ScoreNumbersAnimation.ScoreAnimationFinished += ScoreNumbersAnimation_ScoreAnimationFinished;
    }

    private void OnDisable()
    {
        ScoreNumbersAnimation.ScoreAnimationFinished -= ScoreNumbersAnimation_ScoreAnimationFinished;
    }


    public void Play(string seconds, string user)
    {
        gameObject.SetActive(true);
        _secondsStats.text = seconds;
        _userNameStats.text = user;
        
        _bgDirector.Play();
    }

    public void RestartBGLoop()
    {
        _bgDirector.time = (float)_loopBGFromFrame / _fps ;
    }

    private void ScoreNumbersAnimation_ScoreAnimationFinished()
    {
        _deathScreenButtonsUIControl.TimelinePlay();
        _buttons.SetActive(true);
    } 
}
