using UnityEngine;

/// <summary>
/// Call vibration based on events related to Player Character.
/// </summary>
public class PlayerVibratingHit : MonoBehaviour
{
    private bool _isDamageable = true;
    private void OnEnable()
    {
        PlayerCollisionHandler.EnemyCollided += PlayerCollisionHandler_EnemyCollided;
        PlayerHealth.PlayerInvincibilityFinished += PlayerHealth_PlayerInvincibilityFinished;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.EnemyCollided -= PlayerCollisionHandler_EnemyCollided;
        PlayerHealth.PlayerInvincibilityFinished -= PlayerHealth_PlayerInvincibilityFinished;
    }
    
    /// <summary>
    /// Vibrate
    /// </summary>
    void PlayerCollisionHandler_EnemyCollided()
    {
        if (_isDamageable)
        {
            Handheld.Vibrate();
            _isDamageable = false;
        }
    }
    
    /// <summary>
    /// Allow Vibration only if player can be damaged
    /// </summary>
    private void PlayerHealth_PlayerInvincibilityFinished()
    {
        _isDamageable = true;
    }
}
