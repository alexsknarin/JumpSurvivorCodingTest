using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _spawnPos = Vector3.zero;
    private float _direction;
   
    void Start()
    {
        _spawnPos.x = 20f;
        SpawnSetup(1);
    }

    // Update is called once per frame
    void Update()
    {
        Move();        
    }

    public void SpawnSetup(float dir)
    {
        _spawnPos.x *= -dir;
        _spawnPos.y = 0.5f;
        transform.position = _spawnPos;
        _direction = dir;
    }

    private void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime *_speed * _direction);
    }
    
}
