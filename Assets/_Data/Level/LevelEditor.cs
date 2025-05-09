using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
public class LevelEditorWindow : EditorWindow
{
    private LevelData levelData;

    [MenuItem("GameTools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tetris Level Editor", EditorStyles.boldLabel);

        levelData = (LevelData)EditorGUILayout.ObjectField("Level Data", levelData, typeof(LevelData), false);

        if (levelData != null)
        {
            levelData.levelID = EditorGUILayout.IntField("Level ID", levelData.levelID);
            levelData.gridWidth = EditorGUILayout.IntField("Grid Width", levelData.gridWidth);
            levelData.gridHeight = EditorGUILayout.IntField("Grid Height", levelData.gridHeight);
            levelData.garbageRowCount = EditorGUILayout.IntField("Garbage Row Count", levelData.garbageRowCount);
            levelData.chestCount = EditorGUILayout.IntField("Chest Count", levelData.chestCount);

            EditorGUILayout.Space();

            if (GUILayout.Button("Export to JSON"))
            {
                ExportLevelToJson(levelData);
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(levelData);
            }
        }
    }

    private void ExportLevelToJson(LevelData level)
    {
        LevelDataJson jsonData = new LevelDataJson()
        {
            levelID = level.levelID,
            gridWidth = level.gridWidth,
            gridHeight = level.gridHeight,
            garbageRowCount = level.garbageRowCount,
            chestCount = level.chestCount
        };

        string json = JsonUtility.ToJson(jsonData, true);
        string path = EditorUtility.SaveFilePanel("Save Level JSON", Application.dataPath, "Level" + level.levelID, "json");

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, json);
            Debug.Log("Level exported to: " + path);
        }
    }
}
#endif
