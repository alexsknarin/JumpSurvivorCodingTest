using UnityEngine;

/// <summary>
/// Call vibration based on events related to Player Character.
/// </summary>
public class PlayerVibratingHit : MonoBehaviour
{
    private bool _isDamageable = true;
    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += Vibrate;
        PlayerHealth.PlayerInvincibilityFinished += StopInvincibility;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= Vibrate;
        PlayerHealth.PlayerInvincibilityFinished -= StopInvincibility;
    }
    
    void Vibrate()
    {
        if (_isDamageable)
        {
            Handheld.Vibrate();
            _isDamageable = false;
        }
    }

    private void StopInvincibility()
    {
        _isDamageable = true;
    }
}
