using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public static event Action OnEnemyCollided;
    public static event Action OnGroundCollided; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnEnemyCollided?.Invoke();
        }
        if (other.gameObject.CompareTag("ground"))
        {
            OnGroundCollided?.Invoke();
        }
    }
}
