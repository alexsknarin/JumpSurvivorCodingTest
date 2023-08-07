using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private IntVariable _maxHealth;
    [SerializeField] private TextMeshProUGUI _healthText;

    void Update()
    {
        _healthText.text = "Health: " + _playerHealth.Value.ToString();
    }
}
