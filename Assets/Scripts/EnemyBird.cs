using UnityEngine;

/// <summary>
/// Initialization, spawn procedure and Movement algorithm of a Bird.
/// </summary>
public class EnemyBird : Enemy
{
    [SerializeField] private float _midLevel = 5f;
    [SerializeField] private float _amplitude = 2f;
    [SerializeField] private float _frequency = 0.5f;
    private float _phase;
    private Vector3 _sinPos;
    [SerializeField] private GameObject _birdView; 
    private Vector3 _birdScale = Vector3.one;
    public override string EnemyName => "Bird";
    
    public override void SpawnSetup(float dir)
    {

        _spawnPos.x = 15.05f;
        _spawnPos.x *= -dir;
        _spawnPos.y = 5f;
        transform.position = _spawnPos;
        _direction = dir;
        _phase = 0.0f;
        _sinPos.x = _spawnPos.x;
        _sinPos.y = _midLevel;
        this.gameObject.SetActive(true);
        
    }

    protected override void Move()
    {
        if (!_isPaused)
        {
            _sinPos.x = transform.position.x;
            _sinPos.x += _direction * _speed * Time.deltaTime;
            _sinPos.y = Mathf.Sin(_phase * _frequency) * _amplitude + _midLevel;

            transform.position = _sinPos;
            
            _birdScale.x = _direction;
            _birdView.transform.localScale = _birdScale;

            _phase += Time.deltaTime;    
        }
    }
}
