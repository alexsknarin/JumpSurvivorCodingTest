using UnityEngine;

/// <summary>
/// Component to control a visual representation of the Player.
/// </summary>
[RequireComponent(typeof(PlayerMovement))]
public class PlayerViewHandler : MonoBehaviour
{
    //[SerializeField] private int _direction = 1;
    [SerializeField] private GameObject _playerViewBase;
    private Vector3 _xFlip = Vector3.one;
    [SerializeField] private Animator _catBodyAnimator;
    [SerializeField] private PlayerMovement _playerMovement;
    // String Property Caching
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int JumpPhase = Animator.StringToHash("jumpPhase");
    private static readonly int OnGround = Animator.StringToHash("onGround");

    private void OnEnable()
    {
        PlayerCollisionHandler.GroundCollided += PlayerCollisionHandler_GroundCollided;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.GroundCollided -= PlayerCollisionHandler_GroundCollided;
    }
   
    private void Update()
    {
        // Flip
        if (_playerMovement.Speed > 0)
        {
            _xFlip.x = 1;
        }
        else if (_playerMovement.Speed < 0)
        {
            _xFlip.x = -1;
        }
        _playerViewBase.transform.localScale = _xFlip;
        
        // Anim control
        _catBodyAnimator.SetFloat(Speed, Mathf.Abs(_playerMovement.Speed));
        _catBodyAnimator.SetFloat(JumpPhase, _playerMovement.JumpPhase);

        if (_playerMovement.JumpPhase > 0.1f)
        {
            _catBodyAnimator.SetBool(OnGround, false);
        }
    }
    
    private void PlayerCollisionHandler_GroundCollided()
    {
        _catBodyAnimator.SetBool(OnGround, true);
    }
}
