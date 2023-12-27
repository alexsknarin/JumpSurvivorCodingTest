using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn/SpawnState", fileName = "SpawnState")]
public class SpawnState : ScriptableObject
{
    [SerializeField] private int _stateId;
    public int StateId => _stateId;
    [SerializeField] private float _stateDuration;
    public float StateDuration => _stateDuration;
    [SerializeField] private bool _useStateDelay;
    public bool UseStateDelay => _useStateDelay;
    [SerializeField] private float _stateDelay;
    public float StateDelay => _stateDelay;

    [Header("---------------------------------")] 
    [SerializeField] private bool _dogsEnabled = false;
    public bool DogsEnabled => _dogsEnabled;
    [SerializeField] private float _dogSpawnRate;
    public float DogSpawnRate => _dogSpawnRate;
    
    [SerializeField] private bool _kangaroosEnabled = false;
    public bool KangaroosEnabled => _kangaroosEnabled;
    [SerializeField] private float _kangarooSpawnRate;
    public float KangarooSpawnRate => _kangarooSpawnRate;
    
    [SerializeField] private bool _birdsEnabled = false;
    public bool BirdsEnabled => _birdsEnabled;
    [SerializeField] private float _birdSpawnRate;
    public float BirdSpawnRate => _birdSpawnRate;
    
}
