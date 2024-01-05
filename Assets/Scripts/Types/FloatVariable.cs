/* Truing to use Scriptable object as a container for the data that is shared between different objects and
 * is also persistent during the whole game lifecycle (are still available after new scene is loaded).
 */
using System;
using UnityEngine;

/// <summary>
/// Scriptable Object to store persistent data of the float type;
/// </summary>
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
