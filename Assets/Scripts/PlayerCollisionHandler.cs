using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public delegate void OnEnemyCollideAction();
    public static event OnEnemyCollideAction OnEnemyCollided;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            if (OnEnemyCollided != null)
            {
                OnEnemyCollided();    
            }
        }
    }
}
