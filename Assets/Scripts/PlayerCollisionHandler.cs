using System;
using UnityEngine;

/// <summary>
/// Check for Player collisions and provide events.
/// </summary>
public class PlayerCollisionHandler : MonoBehaviour
{
    public static event Action OnEnemyCollided;
    public static event Action OnGroundCollided;

    public delegate void BonusCollided(int enemy, Vector3 collisionPosition);
    public static event BonusCollided OnBonusCollided;
    
    // Collision Analytics data
    public delegate void AnalyticsEnemyCollided(string enemyName);
    public static event AnalyticsEnemyCollided OnAnalyticsEnemyCollided;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnEnemyCollided?.Invoke();
            OnAnalyticsEnemyCollided?.Invoke(other.GetComponent<Enemy>().EnemyName);
        }
        if (other.gameObject.CompareTag("ground"))
        {
            OnGroundCollided?.Invoke();
        }
        if (other.gameObject.CompareTag("KangarooBonus"))
        {
            OnBonusCollided?.Invoke(0, transform.position);
        }
        if (other.gameObject.CompareTag("BirdBonus"))
        {
            OnBonusCollided?.Invoke(1, transform.position);
        }
    }
}
