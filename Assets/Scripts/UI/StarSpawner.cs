using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour, IPausable
{
    [SerializeField] private List<StarUIAnimation> _stars;

    public void Play()
    {
        foreach (var star in _stars)
        {
            star.StartAnimation();
        }
    }

    public void SpawnSetup(Vector2 position)
    {
        transform.position = position;
        transform.localScale = transform.localScale * 0.33f; 
        transform.eulerAngles = Vector3.forward * Random.Range(0f, 360f); 
        gameObject.SetActive(true);
        Play();
    }

    public void SetPaused()
    {
    }

    public void SetUnpaused()
    {
    }
}
