using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveContainer
{
    public List<SaveData> Entries = new List<SaveData>();

    public void AddEntry(SaveData entry)
    {
        Debug.Log(entry.Name + ' ' + entry.Time.ToString());
        Entries.Add(entry);
    }

    public void SortScores()
    {
        Entries.Sort(delegate(SaveData x, SaveData y)
        {
            return y.Time.CompareTo(x.Time);
        });
    }
}
