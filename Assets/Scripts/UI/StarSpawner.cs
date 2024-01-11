using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StarSpawner : MonoBehaviour, IPausable
{
    [SerializeField] private List<StarUIAnimation> _stars;
    [SerializeField] private float _animDuration;
    
    public void Play()
    {
        gameObject.SetActive(true);
        foreach (var star in _stars)
        {
            star.StartAnimation();
        }
    }

    public async void SpawnSetup(Vector2 position)
    {
        transform.position = position;
        transform.localScale = Vector3.one * 0.33f; //???
        transform.eulerAngles = Vector3.forward * Random.Range(0f, 360f); 
        gameObject.SetActive(true);
        Play();
        await Task.Delay((int)(_animDuration * 1000));
        gameObject.SetActive(false);
    }

    public void SetPaused()
    {
    }

    public void SetUnpaused()
    {
    }
}
