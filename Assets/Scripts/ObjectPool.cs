using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> _pooledObjects;
    private GameObject _currentObject;
    private int _poolAmount;

    public ObjectPool(int amount, GameObject prefab)
    {
        _pooledObjects = new List<GameObject>();
        _poolAmount = amount;
        for (int i = 0; i < amount; i++)
        {
            _currentObject = GameObject.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
            _currentObject.SetActive(false);
            _pooledObjects.Add(_currentObject);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _poolAmount; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }
}
