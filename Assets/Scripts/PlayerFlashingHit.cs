using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashingHit : MonoBehaviour
{
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _flashFreq;
    [SerializeField] private float _flashDuration;
    [SerializeField] private SpriteRenderer _catSpriteRenderer;
    private float _flashValue;
    private Material _material;
    private bool _isFlashing;
    private float _prevTime;
    

    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += StartFlashing;
        PlayerHealth.OnPlayerInvincibilityFinished += StopFlashing;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= StartFlashing;
        PlayerHealth.OnPlayerInvincibilityFinished -= StopFlashing;
        _material.SetColor("_Color", Color.white);
        _material.SetFloat("_HitMix", 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        _material = _catSpriteRenderer.sharedMaterial;
        _isFlashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFlashing)
        {
            Flashing();    
        }
    }

    private void Flashing()
    {
        _flashValue = (Mathf.Sin(_gameTime.Value * _flashFreq) + 1f) / 2f; 
        _material.SetColor("_Color", _flashColor);
        _material.SetFloat("_HitMix", _flashValue);
    }

    private void StartFlashing()
    {
        _isFlashing = true;
    }

    private void StopFlashing()
    {
        _isFlashing = false;
        _material.SetColor("_Color", Color.white);
        _material.SetFloat("_HitMix", 0f);
    }
}
