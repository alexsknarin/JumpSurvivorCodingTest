/* This is a single saving entry for a player - name and time (score).
 * technically it could be a struct but in this case it is not that important
 * because I don't have any use for it as a value type.
 */

[System.Serializable]
public class SaveData
{
    public string Name;
    public int Time;
}
