using System;
using UnityEngine;

public class CarSuspensionAnimation : MonoBehaviour
{
    [SerializeField] private Transform _carBody;
    [SerializeField] private Collider2D _carBodyCollider;
    [SerializeField] private Transform _roosterTransform;
    [SerializeField] private Transform _frontWheel;
    [SerializeField] private Transform _backWheel;
    [SerializeField] private float _suspensionAmplitude;
    [SerializeField] private float _suspensionFrequency;
    [SerializeField] private float _roosterSinOffset;
    [SerializeField] private float _movementSpeed;
    
    private Vector3 _initialBodyPosition;
    private Vector3 _initialRoosterPosition;

    private void Start()
    {
        _initialBodyPosition = _carBody.localPosition;
        _initialRoosterPosition = _roosterTransform.localPosition;
    }

    private void Update()
    {
        float time = Time.time * _movementSpeed;
        Vector3 bodyPosition = _initialBodyPosition;
        bodyPosition.y += Mathf.Sin(time * _suspensionFrequency) * _suspensionAmplitude;
        _carBody.localPosition = bodyPosition;
        _carBodyCollider.offset = bodyPosition;
        
        Vector3 roosterPosition = _initialRoosterPosition;
        roosterPosition.y += Mathf.Sin(time * _suspensionFrequency + _roosterSinOffset) * _suspensionAmplitude;
        _roosterTransform.localPosition = roosterPosition;
        
        Vector3 wheelLocalRotation = Vector3.zero;
        wheelLocalRotation.z = time * _movementSpeed * 360f;
        _frontWheel.localRotation = Quaternion.Euler(wheelLocalRotation);
        _backWheel.localRotation = Quaternion.Euler(wheelLocalRotation);
    }
}
