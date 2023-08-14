using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaveScores : MonoBehaviour
{
    [SerializeField] private StringVariable _playerName;
    [SerializeField] private IntVariable _difficultyLevel;
    [SerializeField] private FloatVariable _gameTime;
    private string _scoreFolder = "/SaveData/";
    private string _baseFileName = "saveSoreFile.json";
    private SaveContainer _saveContainer = new SaveContainer();
    private SaveData _saveData = new SaveData();

    

    private void SaveSoreData()
    {
        // Generate file path
        string fullSaveFolderPath = Application.persistentDataPath + _scoreFolder;
        if (!Directory.Exists(fullSaveFolderPath))
        {
            Directory.CreateDirectory(fullSaveFolderPath); 
        }
        string fullSaveFilePath = fullSaveFolderPath + Game.GetDifficultyLevelName(_difficultyLevel.Value) + "_" + _baseFileName;
        Debug.Log(fullSaveFilePath);
        

        _saveContainer.Entries.Clear();
        string json;
        if (File.Exists(fullSaveFilePath))
        {
            json = File.ReadAllText(fullSaveFilePath);
            _saveContainer = JsonUtility.FromJson<SaveContainer>(json);
        }

        _saveData.Name = _playerName.Value;
        _saveData.Time = (int)_gameTime.Value;
        _saveContainer.AddEntry(_saveData);
        _saveContainer.SortScores();
        json = JsonUtility.ToJson(_saveContainer);
      
        File.WriteAllText(fullSaveFilePath, json);
    }
}
