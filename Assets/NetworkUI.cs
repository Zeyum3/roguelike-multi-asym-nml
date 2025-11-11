using Unity.Netcode;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    private NetworkManager netMgr;

    void Start()
    {
        netMgr = NetworkManager.Singleton;
        if (netMgr == null)
            Debug.LogError("⚠️ Aucun NetworkManager trouvé dans la scène !");
    }

    void OnGUI()
    {
        if (netMgr == null) return; // évite le crash

        GUILayout.BeginArea(new Rect(10, 10, 300, 200));

        if (!netMgr.IsClient && !netMgr.IsServer)
        {
            if (GUILayout.Button("Start Host"))
                netMgr.StartHost();

            if (GUILayout.Button("Start Server"))
                netMgr.StartServer();

            if (GUILayout.Button("Start Client"))
                netMgr.StartClient();
        }
        else
        {
            if (GUILayout.Button("Shutdown"))
                netMgr.Shutdown();
        }

        GUILayout.EndArea();
    }
}
