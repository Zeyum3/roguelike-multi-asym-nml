using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;



public class NetworkPhotonUI : MonoBehaviourPunCallbacks
{
    [Header("UI Elements")]
    public Button createButton;
    public Button joinButton;
    public TMP_InputField roomInput;

    [Header("Settings")]
    public byte maxPlayers = 4;

    void Start()
    {
        // Connecte automatiquement au serveur Photon
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("🌐 Connecting to Photon...");

        // Assigner les callbacks des boutons
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("✅ Connected to Photon Master Server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateRoom()
    {
        string roomName = string.IsNullOrEmpty(roomInput.text) ? "Room_" + Random.Range(1000, 9999) : roomInput.text;
        RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers };
        PhotonNetwork.CreateRoom(roomName, options);
        Debug.Log("🚀 Creating room: " + roomName);
    }

    public void JoinRoom()
    {
        string roomName = string.IsNullOrEmpty(roomInput.text) ? "Room_" + Random.Range(1000, 9999) : roomInput.text;
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("🔗 Joining room: " + roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("🎯 Joined room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"❌ Join room failed: {message}");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("🎯 Room created successfully: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"❌ Room creation failed: {message}");
    }
}
