using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallSpawner : NetworkBehaviour
{

    public GameObject ballPrefab;

    public void SpawnBall()
    {
        Vector3 spawnPosition1 = new Vector3(0.0f, 50.0f, 0.0f);
        Vector3 spawnPosition2 = new Vector3(0.0f, 50.0f, -8000.0f);
        Quaternion spawnRotation = Quaternion.identity;
        var ball1 = (GameObject)Instantiate(ballPrefab, spawnPosition1, spawnRotation);
        var ball2 = (GameObject)Instantiate(ballPrefab, spawnPosition2, spawnRotation);
        NetworkServer.Spawn(ball1);
        NetworkServer.Spawn(ball2);
    }
}
