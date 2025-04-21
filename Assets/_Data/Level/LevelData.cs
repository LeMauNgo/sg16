using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData : ScriptableObject
{
    public int levelID;
    public int gridWidth = 10;
    public int gridHeight = 20;
    public int requiredKeys;
    public int requiredCombos;
    public int maxTimeSeconds;
    public string levelHint;
    public bool allowSpecialBlocks;

    public List<BlockSpawnData> initialBlocks = new List<BlockSpawnData>();
}