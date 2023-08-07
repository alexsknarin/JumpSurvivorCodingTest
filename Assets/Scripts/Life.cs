using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private int _lives = 50;
    [SerializeField] private TextMeshProUGUI _livesText;
    public int Lives => _lives;
    
    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += DecreaseLives;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= DecreaseLives;
    }

    private void UpdateUI()
    {
        _livesText.text = "Life: " + _lives.ToString().PadLeft(2);
    }
    private void Start()
    {
        UpdateUI();
    }

    private void DecreaseLives()
    {
        _lives -= 1;
        UpdateUI();
    }
}
