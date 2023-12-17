using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO Variables/StringVariable")]
public class StringVariable : ScriptableObject
{
    [SerializeField] private string _value;
    [SerializeField] private bool _readOnly = true;
    public String Value
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