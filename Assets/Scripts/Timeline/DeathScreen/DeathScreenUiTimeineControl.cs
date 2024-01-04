using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;

/// <summary>
/// Control animation of Death screen art assets using Timeline.
/// </summary>
public class DeathScreenUiTimeineControl : MonoBehaviour
{
    [SerializeField] private PlayableDirector _bgDirector;
    [SerializeField] private float _loopFrameStart;
    private float _fps => (float)((TimelineAsset)_bgDirector.playableAsset).editorSettings.frameRate;
    
    public void MainMenuLoopContinue()
    {
        _bgDirector.time = _loopFrameStart / _fps;
    }

    public void TimelinePlay()
    {
        _bgDirector.Play();
    }

}
