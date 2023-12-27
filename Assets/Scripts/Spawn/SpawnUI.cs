using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void ChangeText(string newText)
    {
        _text.text = newText;
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
