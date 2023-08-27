using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyMovement/KangarooWaitState", fileName = "kangarooWaitState")]
public class KangarooWaitState : PlayerMovementBaseState
{
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private float _waitTime;
    private float _prevTime;
    private float _direction;

    public void SetDirection(float direction)
    {
        _direction = direction;
    }

    public override void EnterState()
    {
        Vector3 waitPos = _transform.position;
        waitPos.y = 1f;
        _transform.position = waitPos;
        _prevTime = _gameTime.Value;
    }
    
    public override void ExecuteState()
    {
        float deltaTime = _gameTime.Value - _prevTime;
        if (deltaTime > _waitTime)
        {
            //
        }
    }
}
