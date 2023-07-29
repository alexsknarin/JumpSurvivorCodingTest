using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashingHit : MonoBehaviour
{
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _flashFreq;
    private float _flashValue;
    private Material _material;
    
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _flashValue = (Mathf.Sin(Time.time * _flashFreq) + 1f) / 2f; 
        _material.color = Color.Lerp(Color.white, _flashColor, _flashValue);
    }
}
