using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : Enemy
{
    public override EnemyTypes EnemyType => EnemyTypes.Car;
    [SerializeField] private float _size = 0.85f;
    [SerializeField] private Color[] _colors;
    [SerializeField] private SpriteRenderer _carSprite;
    [SerializeField] private TrafficLight _trafficLight;
    private Material _carMaterial;
    

    private void Awake()
    {
        _carMaterial = _carSprite.sharedMaterial;
    }

    public override void SetupSpawn(float dir)
    {
        gameObject.SetActive(true);
        _direction = dir;
        _spawnPos.x = 30f * dir;
        _carMaterial.color = _colors[UnityEngine.Random.Range(0, _colors.Length)];
        Vector3 scale = Vector3.one * _size;
        scale.x *= -dir;
        transform.localScale = scale;
        _trafficLight.SetMode(TrafficLightsModes.Green);
    }

    protected override void Move()
    {
        if (!_isPaused)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * _speed * 11 * _direction));
        }
    }

    private void OnDisable()
    {
        _trafficLight.SetMode(TrafficLightsModes.Red);
    }
}
