using System;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public event Action OnLogoFadeIn;
    public event Action OnLogoEnd;
    
    public void FadeInLogo()
    {
        OnLogoFadeIn?.Invoke();
    }
    
    public void EndLogo()
    {
        OnLogoEnd?.Invoke();
    }
}
