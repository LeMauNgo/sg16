using UnityEditor.U2D;
using UnityEngine;

public class LevelCtrl : SaiBehaviour
{
    public GameObject garbageBlockPrefab; // Gán trong Unity

    private void SpawnGarbageRows(LevelData levelData)
    {
        int width = levelData.gridWidth;
        int rows = levelData.garbageRowCount;
        int rowStartY = 0;

        for (int y = 0; y < rows; y++)
        {
            int holeX = Random.Range(0, width); // Chỉ để 1 ô trống

            for (int x = 0; x < width; x++)
            {
                if (x != holeX)
                {
                    Vector3Int pos = new Vector3Int(x, rowStartY + y);
                    SpawnGarbageBlock(pos);
                }
            }
        }
    }

    private void SpawnGarbageBlock(Vector3Int pos)
    {
        GameObject block = Instantiate(garbageBlockPrefab, pos, Quaternion.identity);
        // Nếu có hệ thống grid thì gán block vào lưới ở đây
    }
}
