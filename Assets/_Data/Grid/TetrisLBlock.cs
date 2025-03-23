using UnityEngine;

public class TetrisLBlock : TetrisStick
{
    protected static Vector3Int[,] RotationOffsets { get; } = new Vector3Int[4, 4]
    {
            { new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0), new Vector3Int(2, 0, 0) }, // 0°
            { new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, 2, 0), new Vector3Int(2, 2, 0) }, // 90°
            { new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0), new Vector3Int(0, 2, 0) }, // 180°
            { new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, 2, 0), new Vector3Int(0, 0, 0) }  // 270°
    };
    protected override void SetCells(int state)
    {
        for (int i = 0; i < 4; i++)
        {
            cells[i] = RotationOffsets[state, i] + position;
        }
    }
    public override void Rotate()
    {
        int newState = (rotationState + 1) % 4;
        SetCells(newState);
        rotationState = newState;
        this.UpdateVisuals();
    }
}
