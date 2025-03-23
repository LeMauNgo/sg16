using UnityEngine;

public class TetrisO : TetrisStick
{
    protected static Vector3Int[,] RotationOffsets { get; } = new Vector3Int[1, 4]
    {
        { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0) } // O không xoay
    };
    protected override void SetCells(int state)
    {
        for (int i = 0; i < 4; i++)
        {
            cells[i] = RotationOffsets[state, i] + position;
        }
    }
}
