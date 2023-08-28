using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public delegate void OnEnemyCollideAction();
    public static event OnEnemyCollideAction OnEnemyCollided;
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         if (OnEnemyCollided != null)
    //         {
    //             OnEnemyCollided();    
    //         }
    //     }
    // }

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
