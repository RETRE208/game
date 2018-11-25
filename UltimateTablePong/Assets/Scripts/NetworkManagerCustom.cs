using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkManagerCustom : NetworkManager
{
    public GameObject player;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        Debug.Log("test");
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public void setPlayer(GameObject player)
    {
        this.player = player;
    }
}
