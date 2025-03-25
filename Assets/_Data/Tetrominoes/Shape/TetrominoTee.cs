using UnityEngine;

public class TetrominoTee : TetrominoCtrl
{
    public override string GetName()
    {
        return TetrominoCode.Tee.ToString();
    }

    protected override void SetOffset()
    {
        this.rotationOffsets = new Vector3Int[4, 4]
        {
             { new Vector3Int(0, 0, 0), new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0) }, // 0° 
            { new Vector3Int(0, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0) }, // 90°
            { new Vector3Int(0, 0, 0), new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0) }, // 180°
            { new Vector3Int(0, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0) }  // 270°
        };
    }
}
