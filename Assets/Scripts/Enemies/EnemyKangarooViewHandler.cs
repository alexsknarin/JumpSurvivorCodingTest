/* Kangaroo has more complex view than a Dog or a Bird, that is why it has a separate component for it.
 */
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Component to control a visual representation of a Kangaroo.
/// </summary>
[RequireComponent(typeof(EnemyKangaroo))]
public class EnemyKangarooViewHandler : MonoBehaviour, IGroundCollidable
{
    [SerializeField] private GameObject _kangarooVeiewBase;
    private Vector3 _xFlip = Vector3.one;
    [SerializeField] private Animator _kangarooAnimator;
    [SerializeField] private Animator _boots1Animator;
    [SerializeField] private Animator _shirt1Animator;
    [SerializeField] private EnemyKangaroo _enemyKangaroo;
    [SerializeField] private VisualEffect _landDustVfx;
    [SerializeField] private VisualEffect _smokeVfx;
    [SerializeField] private Transform _smokeVfxTransform;
    [SerializeField] private Transform _smokeTrailTransform;
    [SerializeField] private TrailRenderer _smokeTrailRenderer;
    // Replacing string constants with int code numbers 
    private static readonly int JumpPhase = Animator.StringToHash("jumpPhase");
    private static readonly int OnGround = Animator.StringToHash("onGround");

    public void Initialize(int direction)
    {
        Vector3 pos = _smokeVfxTransform.localPosition;
        pos.x = Mathf.Abs(pos.x) * direction;
        _smokeVfxTransform.localPosition = pos;
        _smokeTrailTransform.localPosition = pos;
        _smokeTrailRenderer.Clear();
    }

    public void HandleJumpStart()
    {
        _smokeVfx.SendEvent("OnSmokeStart");
    }
    public void HandleJumpEnd()
    {
        _smokeVfx.SendEvent("OnSmokeStop");
    }

    private void Update()
    {
        // Flip
        if (_enemyKangaroo.Speed > 0)
        {
            _xFlip.x = 1;
        }
        else if (_enemyKangaroo.Speed < 0)
        {
            _xFlip.x = -1;
        }
        _kangarooVeiewBase.transform.localScale = _xFlip;
        
        // Anim control
        _kangarooAnimator.SetFloat(JumpPhase, _enemyKangaroo.JumpPhase);
        _boots1Animator.SetFloat(JumpPhase, _enemyKangaroo.JumpPhase);
        _shirt1Animator.SetFloat(JumpPhase, _enemyKangaroo.JumpPhase);

        if (_enemyKangaroo.JumpPhase > 0.1f)
        {
            _kangarooAnimator.SetBool(OnGround, false);
            _boots1Animator.SetBool(OnGround, false);
            _shirt1Animator.SetBool(OnGround, false);
        }
    }

    public void HandleCollision()
    {
        _kangarooAnimator.SetBool(OnGround, true);
        _boots1Animator.SetBool(OnGround, true);
        _shirt1Animator.SetBool(OnGround, true);
        _landDustVfx.SendEvent("OnLand");
    }
}
