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
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= StartFlashing;
        _material.SetColor("_Color", Color.white);
        _material.SetFloat("_HitMix", 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        //_material = GetComponent<MeshRenderer>().material;
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
        //_material.color = Color.Lerp(Color.white, _flashColor, _flashValue);
        _material.SetColor("_Color", _flashColor);
        _material.SetFloat("_HitMix", _flashValue);
        if (_gameTime.Value - _prevTime > _flashDuration)
        {
            _isFlashing = false;
            //_material.color = Color.white;
            _material.SetColor("_Color", Color.white);
            _material.SetFloat("_HitMix", 0f);
        }
        
    }

    private void StartFlashing()
    {
        _isFlashing = true;
        _prevTime = _gameTime.Value;
    }
}
