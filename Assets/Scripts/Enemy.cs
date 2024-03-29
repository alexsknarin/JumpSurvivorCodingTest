/* To save some time Enemy class in this game is quite big with many responsibilities. 
 * It is responsible for:
 * - Initialization.
 * - Movement.
 * - Deactivation if character is outside of the screen.
 * - Spawn related setup.
 * - Controlling the visuals (Dog and Bird).
 *
 * TODO: split this functionality into a separate classes.
 * 
 */
using UnityEngine;

/// <summary>
/// Base class responsible for general data structure and deactivation handling for all enemy classes.
/// </summary>
public abstract class Enemy : MonoBehaviour, IPausable
{
    [SerializeField] private bool _testingMode = false;
    [SerializeField] protected float _speed;
    protected Vector3 _spawnPos = Vector3.zero;
    protected float _direction;
    protected bool _isPaused = false;
    public abstract string EnemyName { get; }

    private void Start()
    {
        if (_testingMode)
        {
            SetupSpawn(1);
        }
    }
    
    private void Update()
    {
        Move();        
    }

    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }
    
    public abstract void SetupSpawn(float dir);

    protected abstract void Move();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("rightBound") && _direction > 0)
        {
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("leftBound") && _direction < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
