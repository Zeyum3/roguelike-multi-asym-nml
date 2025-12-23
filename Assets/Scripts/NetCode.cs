using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class NetCode : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("test", new() 
        { 
        MaxPlayers = 5
        }, TypedLobby.Default );
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity);
    }

}
