using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerHealth _playerHealth;

    public void Initialize()
    {
        _playerMovement.Initialize();
        _playerHealth.Initialize();
    }
    
}
