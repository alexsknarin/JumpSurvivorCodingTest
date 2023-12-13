using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class DeathScreenButtonsUIControl : MonoBehaviour
{
    [SerializeField] private PlayableDirector _buttonsDirector;
    [SerializeField] private float _StartDelay;
    private int _restartScoresMenuMode = 0;
    private float _fps => (float)((TimelineAsset)_buttonsDirector.playableAsset).editorSettings.frameRate;

    public void TimelinePlay()
    {
        _buttonsDirector.Play();
        StartCoroutine(DelayedPlayStart());
    }

    public void PauseTimeline()
    {
        _buttonsDirector.Pause();
    }

    IEnumerator DelayedPlayStart()
    {
        yield return new WaitForSeconds(_StartDelay/_fps);
        DelayedStart();
    }

    private void DelayedStart()
    {
        _buttonsDirector.Play();
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
