/* Very simple visual scripting system for the game progression.
 * I contains of two lists:
 *  - SpawnStatesLearn - all states in this list will be performed in order to have more controllable beginning of the
 * game and tutorial.
 *  - SpawnStatesMainLoop - all state from this list will be performed randomly - specifically for the second part of
 * the game where player needs to survive against endless wave of enemies.
 *
 * All actual logic is in SpawnManager Class. 
 */
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lists of spawnStates that will be plugged into a SpawnManager and serve as a game script.
/// </summary>
[CreateAssetMenu(menuName = "Spawn/SpawnStateCollection", fileName = "SpawnStateCollection")]
public class SpawnStateCollection : ScriptableObject
{
    public List<SpawnState> SpawnStatesLearn;
    
    [Header("---------------------------------")]
    public List<SpawnState> SpawnStatesBeginning;
    public int RepititionCountBeginning; 
    public float CarSpawnPauseDurationBeginning;
    public float CarSpawnTimeMinBeginning;
    public float CarSpawnTimeMaxBeginning;
    
    [Header("---------------------------------")]
    public List<SpawnState> SpawnStatesMiddle;
    public int RepititionCountMiddle;
    public float CarSpawnPauseDurationMiddle; 
    public float CarSpawnTimeMinMiddle;
    public float CarSpawnTimeMaxMiddle;
    
    [Header("---------------------------------")]
    public List<SpawnState> SpawnStatesLate;
    public int RepititionCountLate;
    public float CarSpawnPauseDurationLate;
    public float CarSpawnTimeMinLate;
    public float CarSpawnTimeMaxLate;
    
    [Header("---------------------------------")]
    public List<SpawnState> SpawnStatesMainLoop;
    
    public int GetLearnStateMaxIndex()
    {
        return SpawnStatesLearn.Count-1;
    }
    
    public int GetBeginningStateMaxIndex()
    {
        return SpawnStatesLearn.Count-1 + RepititionCountBeginning;
    }
    
    public int GetMiddleStateMaxIndex()
    {
        return SpawnStatesLearn.Count-1 + RepititionCountBeginning + RepititionCountMiddle;
    }
    
    public int GetLateStateMaxIndex()
    {
        return SpawnStatesLearn.Count-1 + RepititionCountBeginning + RepititionCountMiddle + RepititionCountLate;
    }
}
