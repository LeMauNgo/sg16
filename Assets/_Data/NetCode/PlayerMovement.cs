using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        if (!IsOwner) return; // Chỉ owner điều khiển

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Gọi RPC để đồng bộ vị trí (nếu cần)
        UpdatePositionServerRpc(transform.position);
    }

    [ServerRpc]
    void UpdatePositionServerRpc(Vector3 position)
    {
        transform.position = position;
    }
}
