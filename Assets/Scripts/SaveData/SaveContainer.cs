/* List of uniform save data (just user name and time). Practically it is Dictionary-like structure
 * that is easy to serialize into JSON.
 * This is by SaveScores class.
 */
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a list of Save Data. Allows to sort a list by Time.  
/// </summary>
[System.Serializable]
public class SaveContainer
{
    public List<SaveData> Entries = new List<SaveData>();

    /// <summary>
    /// Add a SaveData to a list of Entries.
    /// </summary>
    /// <param name="entry">SaveData object</param>
    public void AddEntry(SaveData entry)
    {
        Debug.Log(entry.Name + ' ' + entry.Time.ToString());
        Entries.Add(entry);
    }

    /// <summary>
    /// Sort the list of Entries by .Time in descending order. 
    /// </summary>
    public void SortScores()
    {
        Entries.Sort(delegate(SaveData x, SaveData y)
        {
            return y.Time.CompareTo(x.Time);
        });
    }
}
