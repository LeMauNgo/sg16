﻿using UnityEngine;
using UnityEngine.UIElements;

public class TetrisZBlock : TetrisStick
{
    protected static Vector3Int[,] RotationOffsets { get; } = new Vector3Int[2, 4]
    {
        { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0) }, // Ngang
        { new Vector3Int(1, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 0, 0), new Vector3Int(0, 1, 0) }  // Dọc
    };
    protected override void SetCells(int state)
    {
        for (int i = 0; i < 4; i++)
        {
            cells[i] = RotationOffsets[state, i] + position;
        }
    }
}
