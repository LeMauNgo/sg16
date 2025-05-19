using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerUIManager : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startServerButton;
    [SerializeField] private Button joinClientButton;
    [SerializeField] private InputField ipInputField;
    [SerializeField] private InputField portInputField;
    [SerializeField] private Text statusText;

    private NetworkManager networkManager;

    void Start()
    {
        // Lấy reference đến NetworkManager
        networkManager = NetworkManager.Singleton;
        if (networkManager == null)
        {
            Debug.LogError("NetworkManager không tồn tại trong scene!");
            return;
        }

        // Gắn sự kiện cho các nút
        startHostButton.onClick.AddListener(StartHost);
        startServerButton.onClick.AddListener(StartServer);
        joinClientButton.onClick.AddListener(JoinClient);

        // Đăng ký sự kiện từ MultiplayerEvents
        MultiplayerEvents.Instance.OnHostStarted += () => UpdateStatus("Host started successfully!");
        MultiplayerEvents.Instance.OnServerStarted += () => UpdateStatus("Server started successfully!");
        MultiplayerEvents.Instance.OnClientConnected += (clientId) => UpdateStatus($"Connected to server as Client {clientId}!");
        MultiplayerEvents.Instance.OnNewClientJoined += (clientId) => UpdateStatus($"New client {clientId} joined!");
        MultiplayerEvents.Instance.OnClientConnectFailed += () => UpdateStatus("Connection failed!");

        // Thiết lập trạng thái ban đầu
        UpdateStatus("Ready to connect...");
    }

    void OnDestroy()
    {
        // Hủy sự kiện của các nút
        startHostButton.onClick.RemoveListener(StartHost);
        startServerButton.onClick.RemoveListener(StartServer);
        joinClientButton.onClick.RemoveListener(JoinClient);
    }

    void StartHost()
    {
        UpdateStatus("Starting Host...");
        MultiplayerEvents.Instance.StartHost();
        DisableButtons();
    }

    void StartServer()
    {
        UpdateStatus("Starting Server...");
        MultiplayerEvents.Instance.StartServer();
        DisableButtons();
    }

    void JoinClient()
    {
        string ip = "127.0.0.1";
        ushort port = 7777;
        // Cấu hình Transport với IP và Port
        var transport = networkManager.GetComponent<UnityTransport>();
        if (transport != null)
        {
            transport.ConnectionData.Address = ip;
            transport.ConnectionData.Port = port;
        }

        UpdateStatus($"Connecting to server at {ip}:{port}...");
        MultiplayerEvents.Instance.JoinServer();
        DisableButtons();
    }

    void UpdateStatus(string message)
    {
        statusText.text = message;
        Debug.Log(message);
    }

    void DisableButtons()
    {
        startHostButton.interactable = false;
        startServerButton.interactable = false;
        joinClientButton.interactable = false;
    }

    void EnableButtons()
    {
        startHostButton.interactable = true;
        startServerButton.interactable = true;
        joinClientButton.interactable = true;
    }
}
