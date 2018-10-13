using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    static int defaultY = 1200;

    private int currentPlayer = 1;
    private Vector3 playerOnePosition = new Vector3(0, defaultY, 0);
    private Vector3 playerTwoPosition = new Vector3(4900, defaultY, 0);

    private Vector3 cameraPosition;

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            MoveCameraToOtherPlayerField();
        }
	}

    void MoveCameraToOtherPlayerField()
    {
        ChangePlayer();
        if (currentPlayer == 1)
        {
            gameObject.transform.position = playerOnePosition;
        }
        else
        {
            gameObject.transform.position = playerTwoPosition;
        }
    }

    void ChangePlayer()
    {
        if (currentPlayer == 1)
        {
            currentPlayer = 2;
        }
        else
        {
            currentPlayer = 1;
        }
    }
}
