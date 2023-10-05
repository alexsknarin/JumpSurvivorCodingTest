using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : Enemy
{
    [SerializeField] private GameObject _dogView;
    private Vector3 _dogScale = Vector3.one;
    
    public override void SpawnSetup(float dir)
    {
        _spawnPos.x = 20f;
        _spawnPos.x *= -dir;
        _spawnPos.y = 0.5f;
        transform.position = _spawnPos;
        _direction = dir;
        this.gameObject.SetActive(true);
    }

    protected override void Move()
    {
        if (!_isPaused)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * _direction);
            _dogScale.x = _direction;
            _dogView.transform.localScale = _dogScale;
        }
    }
}
