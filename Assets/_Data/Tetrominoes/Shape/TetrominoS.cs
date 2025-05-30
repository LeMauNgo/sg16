using UnityEngine;

public class TetrominoS : TetrominoCtrl
{
    public override string GetName()
    {
        return TetrominoCode.Sshape.ToString();
    }

    protected override void SetOffset()
    {
        this.rotationOffsets = new Vector3Int[2, 4]
        {
            {  new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0) }, // Ngang
            { new Vector3Int(1, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 0, 0), new Vector3Int(0, 1, 0) }
        };
    }
}
