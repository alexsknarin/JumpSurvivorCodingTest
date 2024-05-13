using System;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    [SerializeField] private AdTmp _preAd;
    [SerializeField] private AdTmp _ad;

    public event Action OnAdFinished;
    
    private void OnEnable()
    {
        _preAd.OnAdFinished += PlayAd;
        _ad.OnAdFinished += FinishAd;
    }
    
    private void OnDisable()
    {
        _preAd.OnAdFinished -= PlayAd;
        _ad.OnAdFinished -= FinishAd;
    }
    public void Play()
    {
        _preAd.gameObject.SetActive(true);
        _preAd.PlayAd();
    }

    private void PlayAd()
    {
        _preAd.gameObject.SetActive(false);
        _ad.gameObject.SetActive(true);
        _ad.PlayAd();
    }

    private void FinishAd()
    {
        PlayerPrefs.SetInt("currentAdIndex", 0);
        OnAdFinished?.Invoke();
    }
}
