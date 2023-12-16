using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private List<StarUIAnimation> _stars;

    public void Play()
    {
        foreach (var star in _stars)
        {
            star.StartAnimation();
        }
    }
}
