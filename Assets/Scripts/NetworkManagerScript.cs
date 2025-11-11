using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkPhotonTest : MonoBehaviourPunCallbacks
{
    [Header("Room Settings")]
    public string roomName = "TestRoom";
    public byte maxPlayers = 4;

    void Start()
    {
        // Connecte automatiquement au serveur Photon
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("🌐 Connecting to Photon...");
    }

    // Appelé quand la connexion au serveur Photon est réussie
    public override void OnConnectedToMaster()
    {
        Debug.Log("✅ Connected to Photon Master Server");
        PhotonNetwork.AutomaticallySyncScene = true; // Pour synchroniser les scènes entre joueurs
    }

    // Crée une room
    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers };
        PhotonNetwork.CreateRoom(roomName, options);
        Debug.Log("🚀 Creating room: " + roomName);
    }

    // Rejoindre une room
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("🔗 Joining room: " + roomName);
    }

    // Callbacks
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
        Debug.Log("🎯 Room created successfully: " + roomName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"❌ Room creation failed: {message}");
    }
}
