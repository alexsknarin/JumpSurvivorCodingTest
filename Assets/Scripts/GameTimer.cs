using System;
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
    
    void Start()
    {
        Game.Pausables.Add(this);
    }

    private void OnDestroy()
    {
        _gameTime.Value = 0f;
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
