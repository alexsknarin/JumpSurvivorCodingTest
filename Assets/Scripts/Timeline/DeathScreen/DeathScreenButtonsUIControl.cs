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
    private float _fps => (float)((TimelineAsset)_buttonsDirector.playableAsset).editorSettings.frameRate;

    public void TimelinePlay()
    {
        _buttonsDirector.Play();
    }

    public void PauseTimeline()
    {
        _buttonsDirector.Pause();
    }

    public void SetRestartScoresMenuMode(int mode)
    {
        _restartScoresMenuMode = mode;
        _buttonsDirector.Play();
    }

    public void RestartScoresMenuExecute()
    {
        SceneManager.LoadScene(_restartScoresMenuMode);    
    }
}
