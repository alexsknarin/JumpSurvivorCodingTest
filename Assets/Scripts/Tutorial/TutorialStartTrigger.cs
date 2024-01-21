using System;
using UnityEngine;

public class TutorialStartTrigger : MonoBehaviour
{
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private int _direction;
    public event Action<int> KangarooTriggered; 
    public event Action<int> BirdTriggered; 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<EnemyKangaroo>(out var enemyKangaroo))
        {
            KangarooTriggered?.Invoke(_direction);
        }

        if (other.gameObject.TryGetComponent<EnemyBird>(out var enemyBird))
        {
            BirdTriggered?.Invoke(_direction);
        }
        
    }
}
