using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Indicate a name of the current spawnState on screen. Temporary debug class.
/// </summary>
public class SpawnUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<Color> _randomColors;
    private int _colorIndex = 0;

    private void OnEnable()
    {
        SpawnManager.SpawnStateChanged += SpawnManager_SpawnStateChanged;
    }

    private void OnDisable()
    {
        SpawnManager.SpawnStateChanged -= SpawnManager_SpawnStateChanged;
    }
    
    private void SpawnManager_SpawnStateChanged(string newText)
    {
        _text.text = newText;
        _text.color = _randomColors[_colorIndex];
        _colorIndex++;
        if (_colorIndex > _randomColors.Count - 1)
        {
            _colorIndex = 0;
        }
    }
}
