using System;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private EnemyTutorial _kangarooTutorial;
    [SerializeField] private EnemyTutorial _birdTutorial;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private TutorialStartTrigger _kangarooStartTriggerL;
    [SerializeField] private TutorialStartTrigger _kangarooStartTriggerR;
    [SerializeField] private TutorialStartTrigger _birdStartTriggerL;
    [SerializeField] private TutorialStartTrigger _birdStartTriggerR;
    private int _arrowDirection = 1;

    private void OnEnable()
    {
        _kangarooStartTriggerL.KangarooTriggered += KangarooStartTrigger_KangarooTriggered; 
        _kangarooStartTriggerR.KangarooTriggered += KangarooStartTrigger_KangarooTriggered; 
        _birdStartTriggerL.BirdTriggered += BirdStartTrigger_BirdTriggered;
        _birdStartTriggerR.BirdTriggered += BirdStartTrigger_BirdTriggered;
    }
    
    private void OnDisable()
    {
        _kangarooStartTriggerL.KangarooTriggered -= KangarooStartTrigger_KangarooTriggered;
        _kangarooStartTriggerR.KangarooTriggered -= KangarooStartTrigger_KangarooTriggered;
        _birdStartTriggerL.BirdTriggered -= BirdStartTrigger_BirdTriggered;
        _birdStartTriggerR.BirdTriggered -= BirdStartTrigger_BirdTriggered;
    }

    private void KangarooStartTrigger_KangarooTriggered(int direction)
    {
        _kangarooStartTriggerL.gameObject.SetActive(false);
        _kangarooStartTriggerR.gameObject.SetActive(false);
        EnemyTutorialStart(direction, _kangarooTutorial);
    }
    
    private void BirdStartTrigger_BirdTriggered(int direction)
    {
        _birdStartTriggerL.gameObject.SetActive(false);
        _birdStartTriggerR.gameObject.SetActive(false);
        EnemyTutorialStart(direction, _birdTutorial);
    }
    
    private void EnemyTutorialStart(int direction, EnemyTutorial enemyTutorial)
    {
        if (direction == 1)
        {
            if (_playerTransform.position.x > 6)
            {
                _arrowDirection = 1;
            }
            else
            {
                _arrowDirection = -1;
            }
        }
        else if (direction == -1)
        {
            if (_playerTransform.position.x < -6)
            {
                _arrowDirection = 1;
            }
            else
            {
                _arrowDirection = -1;
            }
        }
        
        enemyTutorial.Perform(direction, _arrowDirection);
    }
}
