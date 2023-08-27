using UnityEngine;

enum KangarooMovementStates
{
    Jump,
    Wait
}

public class EnemyKangaroo : Enemy
{
    [SerializeField] private AnimationCurve _jumpCurve;
    private KangarooMovementStates _kangarooMovementStates;
    private float _prevTime;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpHorizontalSpeed;
    [SerializeField] private float _waitTime;
    private KangarooMovementStates _movementState;
    [SerializeField] private FloatVariable _gameTime;
    
    public override void SpawnSetup(float dir)
    {
        _spawnPos.x = 20f;
        _spawnPos.x *= -dir;
        _spawnPos.y = 1f;
        transform.position = _spawnPos;
        _direction = dir;
        this.gameObject.SetActive(true);
        StartJump();
    }

    protected override void Move()
    {
        if (!_isPaused)
        {
            switch (_movementState)
            {
                case KangarooMovementStates.Jump:
                    Jump();
                    break;
                case KangarooMovementStates.Wait:
                    Wait();
                    break;
            }            
        }
    }

    private void StartJump()
    {
        _prevTime = _gameTime.Value;
        _movementState = KangarooMovementStates.Jump;
    }
    
    private void Jump()
    {
        float deltaTime = _gameTime.Value - _prevTime;
        if (deltaTime < _jumpTime)
        {
            Vector3 jumpPos = transform.position;
            float jumpPhase = (_gameTime.Value - _prevTime) / _jumpTime;
            jumpPos.y = _jumpCurve.Evaluate(jumpPhase) * _jumpHeight + 1f;
            jumpPos.x += _direction * _jumpHorizontalSpeed * Time.deltaTime; 
            transform.position = jumpPos;
        }
        else
        {
            StartWait();
        }
    }

    private void StartWait()
    {
        Vector3 waitPos = transform.position;
        waitPos.y = 1f;
        transform.position = waitPos;
        _prevTime = _gameTime.Value;
        _movementState = KangarooMovementStates.Wait;
    }

    private void Wait()
    {
        float deltaTime = _gameTime.Value - _prevTime;
        if (deltaTime > _waitTime)
        {
            StartJump();
        }
    }
}
