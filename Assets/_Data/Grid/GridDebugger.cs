using System.Collections.Generic;
using UnityEngine;

public class GridDebugger : MonoBehaviour
{
    [SerializeField] public Dictionary<Vector3Int, Transform> gridDebug = new Dictionary<Vector3Int, Transform>();

    //void Update()
    //{
    //    gridDebug.Clear();
    //    for (int x = 0; x < TetrisGrid.width; x++)
    //    {
    //        for (int y = 0; y < TetrisGrid.height; y++)
    //        {
    //            if (TetrisGrid.grid[x, y, 0] != null)
    //            {
    //                gridDebug[new Vector3Int(x, y, 0)] = TetrisGrid.grid[x, y, 0];
    //            }
    //        }
    //    }
    //}
}
