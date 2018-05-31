using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//www.paladinstudios.com/2013/07/10/how-to-create-an-online-multiplayer-game-with-unity/

public class NetworkManager : MonoBehaviour {
    private const string typeName = "UnGame";
    private const string gameName = "Roomme";
    public int NombreJoueur = 4;
    public int Port = 25000;
    private HostData[] hostList;
    public GameObject TestPrebaf;
    //MasterServer.ipAddress = “127.0.0.1″;//si problème de maintenance de unity

    private void StartServer()
    {
        Network.InitializeServer(NombreJoueur, Port, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
        SpawnTest();
    }
    void OnGUI()
    {
        if(!Network.isClient && !Network.isServer)
        {
            if(GUI.Button(new Rect(100,100,250,100),"Star Server"))
            {
                StartServer();
            }

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }

    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }

    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
        SpawnTest();
    }

    private void SpawnTest()
    {
        Network.Instantiate(TestPrebaf, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
    }
}
