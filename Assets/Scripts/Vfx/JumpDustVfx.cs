using UnityEngine;
using UnityEngine.VFX;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform _inputTransform;
    [SerializeField] private JumpTrail  _jumpTrail;
    [SerializeField] private VisualEffect _jumpDustVfx;
    [SerializeField] private Transform _jumpDustVfxTransform;
    [SerializeField] private VisualEffect _landDustVfx;
    [SerializeField] private Transform _landDustVfxTransform;
    
    private void OnEnable()
    {
        PlayerMovement.OnPlayerJump += StartJump;
        PlayerCollisionHandler.GroundCollided += StartLand;
    }
    
    private void OnDisable()
    {
        PlayerMovement.OnPlayerJump -= StartJump;
        PlayerCollisionHandler.GroundCollided -= StartLand;
    }
    
    private void StartJump()
    {
        _jumpTrail.StartRecording();
        
        Vector3 pos = Vector3.zero;
        pos.x = _inputTransform.position.x;
        _jumpDustVfxTransform.position = pos;
        
        _jumpDustVfx.SendEvent("OnJump");
    }

    private void StartLand()
    {
        Vector3 pos = Vector3.zero;
        pos.x = _inputTransform.position.x;
        _landDustVfxTransform.position = pos;
        
        _landDustVfx.SendEvent("OnLand");
    }
}
