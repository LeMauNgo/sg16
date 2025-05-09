using UnityEngine;

public class CubeTetrominoes : CubeCtrl
{
    public override string GetName()
    {
        return CubeCode.CubeTetrominoes.ToString();
    }
    public virtual void SetMoveDownPosition(Vector3Int position)
    {
        this.transform.position += Vector3Int.down;
    }
}
