using System;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public event Action OnLogoEnd;
    
    public void EndLogo()
    {
        OnLogoEnd?.Invoke();
    }
}
