using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] public List<LevelData> Levels /*{ get; private set; } = new List<LevelData>()*/;
    public int CurrentLevelIndex { get; private set; } = 0;
    public LevelData CurrentLevel => Levels.Count > 0 ? Levels[CurrentLevelIndex] : null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllLevels();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadAllLevels()
    {
        Levels.Clear();
        string path = Application.streamingAssetsPath;

        // Tìm tất cả file JSON bắt đầu bằng "level_"
        var files = Directory.GetFiles(path, "level_*.json");
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
        }
        else
        {
            Debug.LogError("Invalid level index: " + index);
        }
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
