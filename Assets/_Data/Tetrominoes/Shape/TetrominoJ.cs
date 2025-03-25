using Unity.VisualScripting;
using UnityEngine;

public class TetrominoJ : TetrominoCtrl
{
    public override string GetName()
    {
        return TetrominoCode.Jshape.ToString();
    }

    protected override void SetOffset()
    {
        this.rotationOffsets = new Vector3Int[4, 4]
        {
            { new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0), new Vector3Int(0, 0, 0) }, // 0° (ngang)
            { new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, 2, 0), new Vector3Int(2, 0, 0) }, // 90° (dọc phải)
            { new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0), new Vector3Int(2, 2, 0) }, // 180° (ngang ngược)
            { new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, 2, 0), new Vector3Int(0, 2, 0) }  // 270° (dọc trái)
        };
    }
}
