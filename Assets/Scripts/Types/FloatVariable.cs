using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO Variables/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float _value;
    [SerializeField] private bool _readOnly = true;
    public float Value
    {
        get { return _value; }
        set
        {
            if (!_readOnly)
            {
                _value = value;
            }
            else
            {
                Debug.LogException(new Exception("Trying to modify ReadOnly variable!"));
            }
        }
    }
}
