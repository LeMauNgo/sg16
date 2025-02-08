using UnityEngine;

public class TetrominoO : TetrominoCtrl
{
    public override string GetName()
    {
        return TetrominoCode.Square.ToString();
    }

    public override bool IsRotatable()
    {
        return false;
    }
}
