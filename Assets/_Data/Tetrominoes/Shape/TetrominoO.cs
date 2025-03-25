using UnityEngine;

public class TetrominoO : TetrominoCtrl
{
    public override string GetName()
    {
        return TetrominoCode.Square.ToString();
    }

    protected override void SetOffset()
    {
        this.rotationOffsets = new Vector3Int[1, 4]
        {
           { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0) }
        };
    }
}
