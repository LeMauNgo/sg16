using Unity.Netcode;
using UnityEngine;
using System;

public class MultiplayerEvents : MonoBehaviour
{
    // Singleton để truy cập dễ dàng từ các script khác
    public static MultiplayerEvents Instance { get; private set; }

    // Sự kiện khi tạo host thành công
    public event Action OnHostStarted;

    // Sự kiện khi tạo server thành công
    public event Action OnServerStarted;

    // Sự kiện khi client tham gia server thành công
    public event Action<ulong> OnClientConnected;

    // Sự kiện khi client tham gia server thất bại
    public event Action OnClientConnectFailed;

    // Sự kiện khi một client mới tham gia vào server
    public event Action<ulong> OnNewClientJoined;

    private NetworkManager networkManager;

    private void Awake()
    {
        // Thiết lập Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Giữ object qua các scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Lấy reference đến NetworkManager
        networkManager = NetworkManager.Singleton;
        if (networkManager == null)
        {
            Debug.LogError("NetworkManager không tồn tại trong scene!");
            return;
        }

        // Đăng ký các sự kiện của NetworkManager
        networkManager.OnClientConnectedCallback += HandleClientConnected;
        networkManager.OnServerStarted += HandleServerStarted;
        networkManager.OnClientDisconnectCallback += HandleClientDisconnect;
    }

    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện để tránh memory leak
        if (networkManager != null)
        {
            networkManager.OnClientConnectedCallback -= HandleClientConnected;
            networkManager.OnServerStarted -= HandleServerStarted;
            networkManager.OnClientDisconnectCallback -= HandleClientDisconnect;
        }
    }

    // Xử lý khi tạo host hoặc server
    private void HandleServerStarted()
    {
        if (networkManager.IsHost)
        {
            OnHostStarted?.Invoke();
            Debug.Log("Host đã được tạo thành công!");
        }
        else if (networkManager.IsServer)
        {
            OnServerStarted?.Invoke();
            Debug.Log("Server đã được tạo thành công!");
        }
    }

    // Xử lý khi client kết nối
    private void HandleClientConnected(ulong clientId)
    {
        if (networkManager.IsClient && clientId == networkManager.LocalClientId)
        {
            OnClientConnected?.Invoke(clientId);
            Debug.Log($"Client {clientId} đã tham gia server thành công!");
        }
        else if (networkManager.IsServer || networkManager.IsHost)
        {
            OnNewClientJoined?.Invoke(clientId);
            Debug.Log($"Client mới {clientId} đã tham gia server!");
        }
    }

    // Xử lý khi client ngắt kết nối hoặc kết nối thất bại
    private void HandleClientDisconnect(ulong clientId)
    {
        if (networkManager.IsClient && clientId == networkManager.LocalClientId)
        {
            OnClientConnectFailed?.Invoke();
            Debug.Log("Kết nối đến server thất bại hoặc bị ngắt!");
        }
    }

    // Hàm để thử tạo host
    public void StartHost()
    {
        if (networkManager != null)
        {
            networkManager.StartHost();
        }
    }

    // Hàm để thử tạo server
    public void StartServer()
    {
        if (networkManager != null)
        {
            networkManager.StartServer();
        }
    }

    // Hàm để thử tham gia server
    public void JoinServer()
    {
        if (networkManager != null)
        {
            networkManager.StartClient();
        }
    }
}