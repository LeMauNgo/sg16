using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData : ScriptableObject
{
    public int levelID;
    public int gridWidth;
    public int gridHeight;
    public int garbageRowCount;
}