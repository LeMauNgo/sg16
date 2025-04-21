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
            levelData.requiredKeys = EditorGUILayout.IntField("Required Keys", levelData.requiredKeys);
            levelData.requiredCombos = EditorGUILayout.IntField("Required Combos", levelData.requiredCombos);
            levelData.maxTimeSeconds = EditorGUILayout.IntField("Max Time (s)", levelData.maxTimeSeconds);
            levelData.levelHint = EditorGUILayout.TextField("Hint", levelData.levelHint);
            levelData.allowSpecialBlocks = EditorGUILayout.Toggle("Allow Special Blocks", levelData.allowSpecialBlocks);

            EditorGUILayout.Space();
            GUILayout.Label("Initial Blocks", EditorStyles.boldLabel);

            for (int i = 0; i < levelData.initialBlocks.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                levelData.initialBlocks[i].x = EditorGUILayout.IntField("X", levelData.initialBlocks[i].x);
                levelData.initialBlocks[i].y = EditorGUILayout.IntField("Y", levelData.initialBlocks[i].y);
                levelData.initialBlocks[i].type = (BlockType)EditorGUILayout.EnumPopup("Type", levelData.initialBlocks[i].type);
                if (GUILayout.Button("Remove"))
                {
                    levelData.initialBlocks.RemoveAt(i);
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Block"))
            {
                levelData.initialBlocks.Add(new BlockSpawnData());
            }

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
            requiredKeys = level.requiredKeys,
            requiredCombos = level.requiredCombos,
            maxTimeSeconds = level.maxTimeSeconds,
            levelHint = level.levelHint,
            allowSpecialBlocks = level.allowSpecialBlocks,
            initialBlocks = level.initialBlocks
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
