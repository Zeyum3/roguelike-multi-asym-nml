using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    private string joinCode = "";

    async void Start()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("✅ Unity Services initialized");
    }

    async void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Create Relay (Host)"))
            {
                try
                {
                    Allocation alloc = await RelayService.Instance.CreateAllocationAsync(4);
                    joinCode = await RelayService.Instance.GetJoinCodeAsync(alloc.AllocationId);
                    Debug.Log($"Relay Join Code: {joinCode}");

                    var relayData = new RelayServerData(alloc, "dtls");
                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);
                    NetworkManager.Singleton.StartHost();
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }

            GUILayout.Label($"Join Code: {joinCode}");

            joinCode = GUILayout.TextField(joinCode, GUILayout.Width(200));

            if (GUILayout.Button("Join Relay (Client)"))
            {
                try
                {
                    var joinAlloc = await RelayService.Instance.JoinAllocationAsync(joinCode);
                    var relayData = new RelayServerData(joinAlloc, "dtls");
                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);
                    NetworkManager.Singleton.StartClient();
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        else
        {
            if (GUILayout.Button("Shutdown"))
                NetworkManager.Singleton.Shutdown();
        }

        GUILayout.EndArea();
    }
}
