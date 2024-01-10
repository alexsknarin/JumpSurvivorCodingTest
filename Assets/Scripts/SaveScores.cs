using UnityEngine;
using System.IO;

/// <summary>
/// Serialize current score as JSON and save it to file. Add new data to the existing stored in the file.
/// </summary>
public class SaveScores : MonoBehaviour
{
    [SerializeField] private StringVariable _playerName;
    [SerializeField] private IntVariable _difficultyLevel;
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private IntVariable _bonusPoints;
    private string _scoreFolder = "/SaveData/";
    private string _baseFileName = "saveSoreFile.json";
    private SaveContainer _saveContainer = new SaveContainer();
    private SaveData _saveData = new SaveData();

    private void OnEnable()
    {
        Game.OnGameOver += SaveSoreData;
    }

    private void OnDisable()
    {
        Game.OnGameOver -= SaveSoreData;
    }

    private void SaveSoreData()
    {
        // Generate file path
        string fullSaveFolderPath = Application.persistentDataPath + _scoreFolder;
        if (!Directory.Exists(fullSaveFolderPath))
        {
            Directory.CreateDirectory(fullSaveFolderPath); 
        }
        string fullSaveFilePath = fullSaveFolderPath + Game.GetDifficultyLevelName(_difficultyLevel.Value) + "_" + _baseFileName;

        _saveContainer.Entries.Clear();
        string json;
        if (File.Exists(fullSaveFilePath))
        {
            json = File.ReadAllText(fullSaveFilePath);
            _saveContainer = JsonUtility.FromJson<SaveContainer>(json);
        }

        _saveData.Name = _playerName.Value;
        _saveData.Time = (int)_gameTime.Value + _bonusPoints.Value;
        _saveContainer.AddEntry(_saveData);
        _saveContainer.SortScores();
        json = JsonUtility.ToJson(_saveContainer);
      
        File.WriteAllText(fullSaveFilePath, json);
    }
}
