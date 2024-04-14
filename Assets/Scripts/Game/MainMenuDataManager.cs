using UnityEngine;

public class MainMenuDataManager : MonoBehaviour
{
    [SerializeField] private IntVariable _difficultyLevelVariable;
    [SerializeField] private StringVariable _userNameVariable;

    public void SaveGameData(int difficulty, string username)
    {
        _difficultyLevelVariable.Value = difficulty;
        _userNameVariable.Value = username;
    }
}
