using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;



public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDog;
    [SerializeField] private float _dogSwawnRate = 2f;
    private float _prevDogTime;
    private ObjectPool _enemyDogPool;
    
    [SerializeField] private GameObject _enemyKangaroo;
    [SerializeField] private float _kangarooSwawnRate = 4f;
    private float _prevKangarooTime;
    private float _kangarooDelay = 2f;
    private float _kangarooDelayTime;
    private ObjectPool _enemyKangarooPool;
    


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
        _prevDogTime = Time.time;
        _enemyDogPool = new ObjectPool(5, _enemyDog);

        _kangarooDelayTime = Time.time;
        _prevKangarooTime = _kangarooDelayTime + _kangarooDelay;
        _enemyKangarooPool = new ObjectPool(3, _enemyKangaroo);
    }

    // Update is called once per frame
    void Update()
    {
        // Dog
        if (Time.time - _prevDogTime > _dogSwawnRate)
        {
            GameObject currentEnemy = _enemyDogPool.GetPooledObject();
            if (currentEnemy != null)
            {
                float dir = GetRandomDirection();
                currentEnemy.GetComponent<Enemy>().SpawnSetup(dir);
                _prevDogTime = Time.time;                
            }
        }
        
        //Kangaroo
        if (Time.time - _kangarooDelayTime > _kangarooDelay)
        {

            if (Time.time - _prevKangarooTime > _kangarooSwawnRate)
            {
                GameObject currentEnemy =  _enemyKangarooPool.GetPooledObject();
                if (currentEnemy != null)
                {
                    float dir = GetRandomDirection();
                    currentEnemy.GetComponent<Enemy>().SpawnSetup(dir);
                    _prevKangarooTime = Time.time;                
                }
                
            }
        }
    }
}
