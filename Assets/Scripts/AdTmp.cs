using System;
using TMPro;
using UnityEngine;

public class AdTmp : MonoBehaviour
{
    [SerializeField] private TMP_Text _adTimerText;
    [SerializeField] private float _adDuration = 15f;
    public event Action OnAdFinished;
    private bool _isAdPlaying = false;
    private float _localTime = 0;
    
    public void PlayAd()
    {
        _localTime = 0;
        _isAdPlaying = true;
    }
    
    void Update()
    {
        if (_isAdPlaying)
        {
            if (_localTime > _adDuration)
            {
                _isAdPlaying = false;
                gameObject.SetActive(false);
                OnAdFinished?.Invoke();
            }
            else
            {
                _localTime += Time.deltaTime;
                _adTimerText.text = ((int)_adDuration - (int)_localTime).ToString();
            }
        }
    }
}
