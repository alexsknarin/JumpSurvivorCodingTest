using UnityEngine;
using UnityEngine.Pool;


public class GenericObjectPoolClient : MonoBehaviour
{
    // Object Pool Support
    private IObjectPool<GameObject> _objectPool;
    public IObjectPool<GameObject> ObjectPool
    {
        set => _objectPool = value;
    }
    
    public void Release()
    {
        if (_objectPool != null)
        {
            _objectPool.Release(gameObject);    
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
