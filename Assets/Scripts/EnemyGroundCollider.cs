using UnityEngine;

/// <summary>
/// Component for enemies that need to test for collision with Ground.  
/// </summary>
public class EnemyGroundCollider : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    private IGroundCollidable _enemyCollidable;

    private void Awake()
    {
        _enemyCollidable = _enemy.GetComponent<EnemyKangarooViewHandler>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            _enemyCollidable.CollidedWIthGround();
        }
    }
}
