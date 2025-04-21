using UnityEngine;
using UnityEditor;

public class LevelDataCreator
{
    [MenuItem("Assets/Create/ScriptableObject/LevelData")]
    public static void CreateLevelData()
    {
        LevelData asset = ScriptableObject.CreateInstance<LevelData>();
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/NewLevelData.asset");

        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
