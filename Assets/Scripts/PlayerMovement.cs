using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPausable
{
    private bool _isPaused;
    [SerializeField] private float _xBound;
    private Vector3 _boundedPos;

    private StateMachine _moveStateMachine = new StateMachine();
    public MovementBaseState _playerMoveState;
    public MovementBaseState _playerJumpState;
    
    // Start is called before the first frame update
    void Start()
    {
        Game.Pausables.Add(this);
        _playerMoveState.Init(this, _moveStateMachine);
        _playerJumpState.Init(this, _moveStateMachine);
        _moveStateMachine.SetState(_playerMoveState);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPaused)
        {
            _moveStateMachine.Execute();
        }
    }

    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }

    public void ApplyBound(Transform xform)
    {
        Vector3 boundedPos = xform.position;
        if (xform.position.x < -_xBound)
        {
            boundedPos.x = -_xBound;
            xform.position = boundedPos;
        }
        else if (xform.position.x > _xBound)
        {
            boundedPos.x = _xBound;
            xform.position = boundedPos;
        }
    }
}
