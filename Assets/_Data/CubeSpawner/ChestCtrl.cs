using UnityEngine;

public class ChestCtrl : CubeTetrominoes
{
    [SerializeField] protected Vector3Int bottomLeftPosition; // vị trí góc dưới bên trái của rương trên grid
    public Vector3Int BottomLeftPosition => bottomLeftPosition;

    public override string GetName()
    {
        return CubeCode.Chest.ToString();
    }
    public override void SetMoveDownPosition(Vector3Int position)
    {
        if (position != bottomLeftPosition) return;
        base.SetMoveDownPosition(position);
        this.bottomLeftPosition += Vector3Int.down;
    }
    public virtual void SetBottomLeftPosition(Vector3Int position)
    {
        bottomLeftPosition = position;
    }
    public void TryOpenChest()
    {
        if (CanOpenChest())
        {
            OpenChest();
        }
        else
        {
            Debug.Log("Chest cannot be opened. Not all blocks are fully visible.");
        }
    }

    private bool CanOpenChest()
    {
        // Kiểm tra trạng thái của 4 ô thuộc rương
        return !GridManager.Instance.IsOccupied(bottomLeftPosition + new Vector3Int(-1,0,0)) &&
               !GridManager.Instance.IsOccupied(bottomLeftPosition + new Vector3Int(-1,1,0)) &&
               !GridManager.Instance.IsOccupied(bottomLeftPosition + new Vector3Int(0, 2, 0)) &&
               !GridManager.Instance.IsOccupied(bottomLeftPosition + new Vector3Int(1, 2, 0)) &&
               !GridManager.Instance.IsOccupied(bottomLeftPosition + new Vector3Int(2, 0, 0)) &&
               !GridManager.Instance.IsOccupied(bottomLeftPosition + new Vector3Int(2, 1, 0));
    }

    protected virtual void OpenChest()
    {

        // Hiệu ứng mở rương (ví dụ animation, sound)
        Debug.Log("Chest opened!");

        // Nhận vật phẩm ngay lập tức
        GiveReward();
        GridManager.Instance.ClearBlock(new Vector3Int(bottomLeftPosition.x, bottomLeftPosition.y));
        GridManager.Instance.ClearBlock(new Vector3Int(bottomLeftPosition.x + 1, bottomLeftPosition.y));
        GridManager.Instance.ClearBlock(new Vector3Int(bottomLeftPosition.x, bottomLeftPosition.y + 1));
        GridManager.Instance.ClearBlock(new Vector3Int(bottomLeftPosition.x + 1, bottomLeftPosition.y + 1));
        this.Despawn.DoDespawn();
    }

    private void GiveReward()
    {
        Debug.Log("Player received a reward!");
        InventoriesManager.Instance.AddItem(ItemCode.Gold, 10);
    }


}