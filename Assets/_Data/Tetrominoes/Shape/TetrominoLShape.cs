using UnityEngine;

public class TetrominoLShape : TetrominoCtrl
{
    public override string GetName()
    {
        return TetrominoCode.LShape.ToString();
    }

    protected override void SetOffset()
    {
        this.rotationOffsets = new Vector3Int[4, 4]
        {
             { new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0), new Vector3Int(2, 0, 0) }, // 0°
            { new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, 2, 0), new Vector3Int(2, 2, 0) }, // 90°
            { new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0), new Vector3Int(0, 2, 0) }, // 180°
            { new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, 2, 0), new Vector3Int(0, 0, 0) }  // 270°
        };
    }

}
