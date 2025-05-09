using UnityEngine;

public class CameraManager : SaiSingleton<CameraManager>
{
    public virtual Vector3Int GetPosTertromino()
    {
        if(transform.position.y >= 85)
        {
            return new Vector3Int(GridManager.Instance.With / 2 - 2, GridManager.Instance.Height - 2, 0);
        }
        else
        {
            Vector3 offset = new Vector3(-2, 8, 0);
            Vector3 position = transform.position + offset;
            position.z = 0; // đảm bảo Z = 0
            return Vector3Int.RoundToInt(position);
        }
    }
}
