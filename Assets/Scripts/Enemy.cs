using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IPausable
{
    [SerializeField] private bool _testingMode = false;
    [SerializeField] protected float _speed;
    protected Vector3 _spawnPos = Vector3.zero;
    protected float _direction;
    protected bool _isPaused = false;

    private void Start()
    {
        if (_testingMode)
        {
            SpawnSetup(1);
        }
    }

    public void SetPaused()
    {
        _isPaused = true;
    }

    public void SetUnpaused()
    {
        _isPaused = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();        
    }

    public abstract void SpawnSetup(float dir);

    protected abstract void Move();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("rightBound") && _direction > 0)
        {
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("leftBound") && _direction < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
