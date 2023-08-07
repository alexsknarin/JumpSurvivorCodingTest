using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private IntVariable _playerHealth;
    [SerializeField] private IntVariable _maxHealth;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private GameObject _lifeBarSize;
    private Vector3 _lifeBarScale = Vector3.one; 

    void Update()
    {
        _healthText.text = "Life: " + _playerHealth.Value.ToString();
        _lifeBarScale.x = (float)_playerHealth.Value / (float)_maxHealth.Value;
        _lifeBarSize.transform.localScale = _lifeBarScale;
    }
}
