using System.Collections.Generic;
using UnityEngine;

public class OnscreenButtonAnimation : MonoBehaviour
{
    [SerializeField] private List<Transform> _buttonImages;
    [SerializeField] private float _pressDepth = .92f;

    public void PerformPress()
    {
        foreach (var buttonImage in _buttonImages)
        {
            buttonImage.localScale = Vector3.one * _pressDepth;
        }
    }
    
    public void PerformRelease()
    {
        foreach (var buttonImage in _buttonImages)
        {
            buttonImage.localScale = Vector3.one;
        }
    }
}
