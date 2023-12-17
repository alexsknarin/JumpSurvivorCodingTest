using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public static event Action OnEnemyCollided;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (OnEnemyCollided != null)
            {
                OnEnemyCollided();    
            }
        }
    }
}
