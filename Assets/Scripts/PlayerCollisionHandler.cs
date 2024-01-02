using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public static event Action OnEnemyCollided;
    public static event Action OnGroundCollided; 
    
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
    }
}
