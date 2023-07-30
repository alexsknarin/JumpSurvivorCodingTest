using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDog;
    private float _spawnFrequency = 2f;
    private float _prevTime;

    private float GetRandomDirection()
    {
        if (Random.Range(-1f, 1f) > 0)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _prevTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _prevTime > _spawnFrequency)
        {
            var currentEnemy = Instantiate(_enemyDog, _enemyDog.transform.position, _enemyDog.transform.rotation);
            float dir = GetRandomDirection();
            currentEnemy.GetComponent<Enemy>().SpawnSetup(dir);
            _prevTime = Time.time;
        }
    }
}
