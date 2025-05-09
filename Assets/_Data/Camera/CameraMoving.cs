using UnityEngine;

public class CameraMoving : ObjMovement
{
    public float followSpeed = 5f; // tốc độ camera di chuyển
    public float yOffset = 2f;     // khoảng cách lệch lên trên để không che block

    public GridManager gridManager; // gán GridManager trong Inspector
    public Transform cameraTarget;  // đối tượng đại diện vị trí camera nên hướng tới (có thể là chính Camera hoặc 1 empty object)

    void Update()
    {
        this.Moving();
    }
    protected override void Moving()
    {
        if (!GameManager.Instance.IsPlaying) return;
        int highestY = GetHighestOccupiedRow();

        if (highestY >= 0)
        {
            // Chuyển tọa độ lưới sang tọa độ thế giới
            Vector3 worldTargetPos = gridManager.GridToWorldPosition(new Vector3Int(gridManager.With / 2, highestY, 0));
            this.targetPosition = new Vector3(
                cameraTarget.position.x,
                worldTargetPos.y + yOffset,
                cameraTarget.position.z
            );

            // Di chuyển camera mượt
            transform.parent.position = Vector3.Lerp(cameraTarget.position, this.targetPosition, followSpeed * Time.deltaTime);
        }
    }
    int GetHighestOccupiedRow()
    {
        for (int y = gridManager.Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridManager.With; x++)
            {
                if (gridManager.GridRows[y].row[x] != null)
                {
                    return y;
                }
            }
        }
        return -1; // không có block nào
    }
}
