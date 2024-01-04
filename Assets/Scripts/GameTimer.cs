using UnityEngine;

/// <summary>
/// Constantly update gameTime Scriptable Object Variable. This is that time that is counted for the scores and displayed on screen.
/// </summary>
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
