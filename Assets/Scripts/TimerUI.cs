using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private FloatVariable _gameTime;

    void Start()
    {
        
    }

    void Update()
    {
        ShowTimeText(FormatTime(_gameTime.Value));
    }

    private void ShowTimeText(string time)
    {
        _timerText.text = "Time: " + time;
    }

    private string FormatTime(float time)
    {
        float minutes = (int)(time/60);
        float seconds = (int)(time%60);
        return minutes.ToString().PadLeft(2, '0') + ':' + seconds.ToString().PadLeft(2, '0');
    }
}