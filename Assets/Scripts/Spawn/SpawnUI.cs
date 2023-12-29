using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<Color> _randomColors;
    private int _colorIndex = 0;

    private void ChangeText(string newText)
    {
        _text.text = newText;
        _text.color = _randomColors[_colorIndex];
        _colorIndex++;
        if (_colorIndex > _randomColors.Count - 1)
        {
            _colorIndex = 0;
        }
    }

    private void OnEnable()
    {
        SpawnManager.OnSpawnStateChanged += ChangeText;
    }

    private void OnDisable()
    {
        SpawnManager.OnSpawnStateChanged -= ChangeText;
    }
}
