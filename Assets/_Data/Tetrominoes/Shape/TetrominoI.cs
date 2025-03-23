using UnityEngine;

public class TetrominoI : TetrominoCtrl
{
    public override string GetName()
    {
        return TetrominoCode.Line.ToString();
    }

    protected override void SetOffset()
    {
        this.rotationOffsets = new Vector3Int[2, 4]
        {
            { new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0), new Vector3Int(3, 1, 0) }, // Horizontal
            { new Vector3Int(2, 0, 0), new Vector3Int(2, 1, 0), new Vector3Int(2, 2, 0), new Vector3Int(2, 3, 0) }  // Vertical
        };
    }
}
