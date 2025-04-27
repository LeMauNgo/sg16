using UnityEngine;

public class ChestCtrl : CubeCtrl
{
    public Vector3Int bottomLeftPosition; // vị trí góc dưới bên trái của rương trên grid

    private bool isOpened = false;
    public override string GetName()
    {
        return CubeCode.Chest.ToString();
    }
    private void Update()
    {
        if (isOpened) return;

        if (CheckChestExposed())
        {
            OpenChest();
        }
    }

    private bool CheckChestExposed()
    {
        // Giả sử bạn có hàm IsOccupied(x, y) để kiểm tra block
        return !IsOccupied(bottomLeftPosition.x, bottomLeftPosition.y) &&
               !IsOccupied(bottomLeftPosition.x + 1, bottomLeftPosition.y) &&
               !IsOccupied(bottomLeftPosition.x, bottomLeftPosition.y + 1) &&
               !IsOccupied(bottomLeftPosition.x + 1, bottomLeftPosition.y + 1);
    }

    private void OpenChest()
    {
        isOpened = true;

        // Hiệu ứng mở rương (ví dụ animation, sound)
        Debug.Log("Chest opened!");

        // Nhận vật phẩm ngay lập tức
        GiveReward();

        // (Tùy bạn) Huỷ rương sau khi mở
        Destroy(gameObject);
    }

    private void GiveReward()
    {
        // Ở đây bạn có thể random vật phẩm thưởng
        // Tạm thời mình làm đơn giản:
        Debug.Log("Player received a reward!");

        // Ví dụ: buff tốc độ, thêm điểm, phá hàng rác, v.v.
        // Bạn có thể code thêm logic ở đây tùy yêu cầu.
    }

    // ====== DUMMY FUNCTION để test =======
    private bool IsOccupied(int x, int y)
    {
        // TODO: Code kiểm tra lưới thật
        return false;
    }


}
