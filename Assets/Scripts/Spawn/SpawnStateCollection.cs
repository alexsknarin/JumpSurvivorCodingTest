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
    public List<SpawnState> SpawnStatesMainLoop;
}
