using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyBird : Enemy
{
    [SerializeField] private float _midLevel = 5f;
    [SerializeField] private float _amplitude = 2f;
    [SerializeField] private float _frequency = 0.5f;
    private float _phase;
    private Vector3 _sinPos;
    
    public override void SpawnSetup(float dir)
    {

        _spawnPos.x = 20f;
        _spawnPos.x *= -dir;
        _spawnPos.y = 5f;
        transform.position = _spawnPos;
        _direction = dir;
        _phase = 0.0f;
        _sinPos.x = _spawnPos.x;
        _sinPos.y = _midLevel;
        this.gameObject.SetActive(true);
        
    }

    protected override void Move()
    {
        if (!_isPaused)
        {
            _sinPos.x = transform.position.x;
            _sinPos.x += _direction * _speed * Time.deltaTime;
            _sinPos.y = Mathf.Sin(_phase * _frequency) * _amplitude + _midLevel;

            transform.position = _sinPos;

            _phase += Time.deltaTime;    
        }
    }
}
