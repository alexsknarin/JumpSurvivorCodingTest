using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn/SpawnStateCollection", fileName = "SpawnStateCollection")]
public class SpawnStateCollection : ScriptableObject
{
    public List<SpawnState> SpawnStates;
}
