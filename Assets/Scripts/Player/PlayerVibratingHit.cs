using UnityEngine;

/// <summary>
/// Call vibration based on events related to Player Character.
/// </summary>
public class PlayerVibratingHit : MonoBehaviour
{
    private bool _isDamageable = true;
    private void OnEnable()
    {
        PlayerCollisionHandler.EnemyCollided += HandleEnemyCollision;
        PlayerHealth.PlayerInvincibilityFinished += EnableDamageable;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.EnemyCollided -= HandleEnemyCollision;
        PlayerHealth.PlayerInvincibilityFinished -= EnableDamageable;
    }
    
    /// <summary>
    /// Vibrate
    /// </summary>
    private void HandleEnemyCollision()
    {
        if (_isDamageable)
        {
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
            _isDamageable = false;
        }
    }
    
    /// <summary>
    /// Allow Vibration only if player can be damaged
    /// </summary>
    private void EnableDamageable()
    {
        _isDamageable = true;
    }
}
