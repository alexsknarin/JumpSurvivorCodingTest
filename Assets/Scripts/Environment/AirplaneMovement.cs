using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneMovement : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;

    void Update()
    {
        _transform.localPosition += Vector3.up * (_speed * Time.deltaTime);
        if (_transform.localPosition.y > 80f)
        {
            Vector3 pos = _transform.localPosition;
            pos.y = 0f;
            _transform.localPosition = pos;
        }
    }
}
