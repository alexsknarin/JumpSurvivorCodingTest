using System;
using UnityEngine;

/// <summary>
/// Check for Player collisions and provide events.
/// </summary>
public class PlayerCollisionHandler : MonoBehaviour
{
    public static event Action EnemyCollided;
    public static event Action GroundCollided;
    public static event Action<int, Vector3> BonusCollided;
    
    // Collision Analytics data
    public static event Action<string> AnalyticsEnemyCollided;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyCollided?.Invoke();
            AnalyticsEnemyCollided?.Invoke(other.GetComponent<Enemy>().EnemyName);
        }
        if (other.gameObject.CompareTag("ground"))
        {
            GroundCollided?.Invoke();
        }
        if (other.gameObject.CompareTag("KangarooBonus"))
        {
            BonusCollided?.Invoke(0, transform.position);
        }
        if (other.gameObject.CompareTag("BirdBonus"))
        {
            BonusCollided?.Invoke(1, transform.position);
        }
    }
}
