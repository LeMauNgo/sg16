using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : SaiSingleton<LevelManager>
{

    [SerializeField] public List<LevelData> Levels /*{ get; private set; } = new List<LevelData>()*/;
    public int CurrentLevelIndex { get; private set; } = 0;
    public LevelData CurrentLevel => Levels.Count > 0 ? Levels[CurrentLevelIndex] : null;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAllLevels();
    }
    void LoadAllLevels()
    {
        Levels.Clear();
        string path = Application.streamingAssetsPath;

        // Tìm tất cả file JSON bắt đầu bằng "level_"
        var files = Directory.GetFiles(path, "Level*.json");
        foreach (var file in files)
        {
            string json = File.ReadAllText(file);
            LevelData level = ScriptableObject.CreateInstance<LevelData>();
            JsonUtility.FromJsonOverwrite(json, level);
            Levels.Add(level);
        }

        Levels.Sort((a, b) => a.levelID.CompareTo(b.levelID));
        Debug.Log($"Loaded {Levels.Count} levels.");
    }

    public void LoadLevel(int index)
    {
        if (index >= 0 && index < Levels.Count)
        {
            CurrentLevelIndex = index;
            Debug.Log("Level loaded: " + CurrentLevel.levelID);
            // Gọi sự kiện hoặc logic khởi tạo level ở đây
            InitLevel(CurrentLevel);
        }
        else
        {
            Debug.LogError("Invalid level index: " + index);
        }
    }
    private void InitLevel(LevelData level)
    {
        GridManager.Instance.GenerateGrid(level.gridWidth, level.gridHeight);
        GridManager.Instance.ClearGrid();
        GridManager.Instance.SpawnChestAndGarbageRows(level);
    }
    public void LoadNextLevel()
    {
        LoadLevel(CurrentLevelIndex + 1);
    }

    public bool HasNextLevel()
    {
        return CurrentLevelIndex + 1 < Levels.Count;
    }
}
