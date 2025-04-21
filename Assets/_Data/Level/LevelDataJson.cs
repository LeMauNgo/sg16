using System.Collections.Generic;

[System.Serializable]
public class LevelDataJson
{
    public int levelID;
    public int gridWidth;
    public int gridHeight;
    public int requiredKeys;
    public int requiredCombos;
    public int maxTimeSeconds;
    public string levelHint;
    public bool allowSpecialBlocks;
    public List<BlockSpawnData> initialBlocks;
}