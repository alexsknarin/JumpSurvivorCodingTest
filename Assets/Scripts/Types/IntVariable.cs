using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO Variables/IntVariable")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int _value;
    [SerializeField] private bool _readOnly = true;
    public int Value
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