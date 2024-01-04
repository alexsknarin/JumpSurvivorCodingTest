using System.IO;
using TMPro;
using UnityEngine;

/// <summary>
/// Component to Handle formatting and displaying high score data on a screen.
/// </summary>
public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private string _difficultyLevel;
    private int _textWidth = 35;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        // Read Json file
        string fullFileName = Application.persistentDataPath + "/SaveData/" + _difficultyLevel + "_saveSoreFile.json";
        if (!File.Exists(fullFileName))
        {
            return;
        }

        string json = File.ReadAllText(fullFileName);
        SaveContainer saveData = JsonUtility.FromJson<SaveContainer>(json);

        string text = _difficultyLevel + ":\n";
        foreach (var entry in saveData.Entries)
        {
            int padding = _textWidth - (entry.Name.Length + entry.Time.ToString().Length + 2);   
            text += entry.Name + " " + "-".PadLeft(padding, '-') + " " + entry.Time + "\n";
        }

        _scoreText.text = text;
    }

}
