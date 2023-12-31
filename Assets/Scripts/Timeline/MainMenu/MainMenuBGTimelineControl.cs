using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MainMenuBGTimelineControl : MonoBehaviour
{
    [SerializeField] private PlayableDirector _bgDirector;
    [SerializeField] private float _loopFrameStart;
    private float _fps => (float)((TimelineAsset)_bgDirector.playableAsset).editorSettings.frameRate;

    public void MainMenuLoopContinue()
    {
        _bgDirector.time = _loopFrameStart / _fps;
    }

    public void Play()
    {
        _bgDirector.Play();
    }
}
