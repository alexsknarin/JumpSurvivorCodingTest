using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPausable
{
    private bool _isPaused;
    [SerializeField] private float _xBound;
    private Vector3 _boundedPos;

    private StateMachine _moveStateMachine = new StateMachine();
    // Those fields are public to be accessible from the state machine
    public PlayerMovementBaseState PlayerMoveState;
    public PlayerMovementBaseState PlayerJumpState;
    
    // Animator Hooks
    public float Speed { get; set; }
    public float JumpPhase { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Game.Pausables.Add(this);
        PlayerMoveState.Init(this, _moveStateMachine);
        PlayerJumpState.Init(this, _moveStateMachine);
        _moveStateMachine.SetState(PlayerMoveState);
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
