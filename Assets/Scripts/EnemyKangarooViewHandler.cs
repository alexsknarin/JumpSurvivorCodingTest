using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyKangaroo))]
public class EnemyKangarooViewHandler : MonoBehaviour
{
    [SerializeField] private GameObject _kangarooVeiewBase;
    private Vector3 _xFlip = Vector3.one;
    [SerializeField] private Animator _kangarooAnimator;
    [SerializeField] private EnemyKangaroo _enemyKangaroo;

    private void Update()
    {
        // Flip
        if (_enemyKangaroo.Speed > 0)
        {
            _xFlip.x = 1;
        }
        else if (_enemyKangaroo.Speed < 0)
        {
            _xFlip.x = -1;
        }
        _kangarooVeiewBase.transform.localScale = _xFlip;
        
        // Anim control
        _kangarooAnimator.SetFloat("jumpPhase", _enemyKangaroo.JumpPhase);
    }
}
