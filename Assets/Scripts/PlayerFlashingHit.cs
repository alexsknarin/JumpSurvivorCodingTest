using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashingHit : MonoBehaviour
{
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _flashFreq;
    [SerializeField] private float _flashDuration;
    private float _flashValue;
    private Material _material;
    private bool _isFlashing;
    private float _prevTime;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += StartFlashing;
    }


    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
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
        _flashValue = (Mathf.Sin(Time.time * _flashFreq) + 1f) / 2f; 
        _material.color = Color.Lerp(Color.white, _flashColor, _flashValue);
        if (Time.time - _prevTime > _flashDuration)
        {
            _isFlashing = false;
            _material.color = Color.white;
        }
        
    }

    private void StartFlashing()
    {
        _isFlashing = true;
        _prevTime = Time.time;
    }
}
