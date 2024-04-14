using System;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Control animation of Death screen Control Buttons using Timeline.
/// </summary>
public class DeathScreenButtonsUIControl : MonoBehaviour
{
    [SerializeField] private PlayableDirector _buttonsDirector;
    private int _restartScoresMenuMode = 0;
    private WaitForSeconds _waitForButtons = new WaitForSeconds(0.5f);
    private float _fps => (float)((TimelineAsset)_buttonsDirector.playableAsset).editorSettings.frameRate;

    public static event Action<int> DeathUIButtonPressed;
    public static event Action OnDeathScreenButtonsAppeared;
    

    public void TimelinePlay()
    {
        _buttonsDirector.Play();
    }

    public void PauseTimeline()
    {
        _buttonsDirector.Pause();
        StartCoroutine(OnAnimationFinished());
    }

    public void SetRestartScoresMenuMode(int mode)
    {
        _restartScoresMenuMode = mode;
        _buttonsDirector.Play();
    }

    public void RestartScoresMenuExecute()
    {
        DeathUIButtonPressed?.Invoke(_restartScoresMenuMode);
    }
    
    private IEnumerator OnAnimationFinished()
    {
        yield return _waitForButtons;
        OnDeathScreenButtonsAppeared?.Invoke();
    }
}
