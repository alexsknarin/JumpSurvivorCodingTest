using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Vector3 _spawnPos = Vector3.zero;
    protected float _direction;
   
    // Update is called once per frame
    void Update()
    {
        Move();        
    }

    public abstract void SpawnSetup(float dir);

    protected abstract void Move();

    private void OnTriggerEnter(Collider other)
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
