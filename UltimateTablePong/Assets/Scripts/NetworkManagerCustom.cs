using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkManagerCustom : NetworkManager
{
    public override void OnServerConnect(NetworkConnection connection)
    {
        base.OnServerConnect(connection);
        if (connection.address != "localClient")
        {
            GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
            GameController gameController = gameControllerObject.GetComponent<GameController>();
            gameController.player2Connected(true);
        }
    }

    public override void OnServerDisconnect(NetworkConnection connection)
    {
        base.OnServerDisconnect(connection);
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        GameController gameController = gameControllerObject.GetComponent<GameController>();
        gameController.player2Connected(false);
    }
}
