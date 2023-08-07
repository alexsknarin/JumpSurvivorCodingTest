using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour, IPausable
{
    [SerializeField] private FloatVariable _gameTime;
    private bool _isPaused;
    
    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _gameTime.Value = 0f;
        Game.Pausables.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPaused)
        {
            _gameTime.Value += Time.deltaTime;
        }
    }
}
