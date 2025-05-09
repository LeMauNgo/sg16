using UnityEngine;
using static UnityEditor.PlayerSettings;

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
    public virtual CubeChestTetrominoesCtrl SpawnChestCube(Vector3Int pos)
    {
        CubeCtrl effectPrefabs = CubeSpawnerManager.Instance.Spanwer.PoolPrefabs.GetByName(CubeCode.CubeChestTetrominoes.ToString());
        CubeCtrl effectCtrl = CubeSpawnerManager.Instance.Spanwer.Spawn(effectPrefabs, pos);
        CubeChestTetrominoesCtrl cubeChestTetrominoes = effectCtrl as CubeChestTetrominoesCtrl;
        cubeChestTetrominoes.SetChestCtrl(this);
        effectCtrl.gameObject.SetActive(true);
        return cubeChestTetrominoes;
    }
    private bool CanOpenChest()
    {
        // Kiểm tra trạng thái của 4 ô thuộc rương
        return !IsOccupied(bottomLeftPosition) &&
               !IsOccupied(bottomLeftPosition + new Vector3Int(1, 0, 0)) &&
               !IsOccupied(bottomLeftPosition + new Vector3Int(0, 1, 0)) &&
               !IsOccupied(bottomLeftPosition + new Vector3Int(1, 1, 0));
    }
    public bool IsOccupied(Vector3Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= GridManager.Instance.With || gridPos.y < 0 || gridPos.y >= GridManager.Instance.Height)
            return true;
        CubeChestTetrominoesCtrl cube = GridManager.Instance.GridRows[gridPos.y].row[gridPos.x] as CubeChestTetrominoesCtrl;
        return cube != null;
    }
    protected virtual void OpenChest()
    {

        // Hiệu ứng mở rương (ví dụ animation, sound)
        Debug.Log("Chest opened!");

        // Nhận vật phẩm ngay lập tức
        GiveReward();
        //GridManager.Instance.ClearBlock(new Vector3Int(bottomLeftPosition.x, bottomLeftPosition.y));
        //GridManager.Instance.ClearBlock(new Vector3Int(bottomLeftPosition.x + 1, bottomLeftPosition.y));
        //GridManager.Instance.ClearBlock(new Vector3Int(bottomLeftPosition.x, bottomLeftPosition.y + 1));
        //GridManager.Instance.ClearBlock(new Vector3Int(bottomLeftPosition.x + 1, bottomLeftPosition.y + 1));
        this.Despawn.DoDespawn();
    }

    private void GiveReward()
    {
        Debug.Log("Player received a reward!");
        InventoriesManager.Instance.AddItem(ItemCode.Gold, 10);
    }


}