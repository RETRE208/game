using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Stick : NetworkBehaviour {

    private float speed;
    public float topBoardBoundary;
    public float bottomBoardBoundary;
    public bool playerOne;
    public bool ai;
    public bool localPlayer;
    private float moveHorizontal;

    private Keybind keybindsMenu;
    private KeyCode p1MoveLeft;
    private KeyCode p1MoveRight;
    private KeyCode p2MoveLeft;
    private KeyCode p2MoveRight;

    private float sensibility1;
    private float sensibility2;

    private void Start()
    {
        speed = 4000.0f;
        moveHorizontal = 0.0f;
        localPlayer = true;
        
        UpdateSensibility1(2.0f);
        UpdateSensibility2(2.0f);
    }

    private void UpdateSensibility1(float sensibility)
    {
        sensibility = (sensibility * 1000) + 2000;
        sensibility1 = sensibility;
    }

    private void UpdateSensibility2(float sensibility)
    {
        sensibility = (sensibility * 1000) + 2000;
        sensibility2 = sensibility;
    }

    public void UpdateControls()
    {
        GameObject keybindController = GameObject.FindGameObjectWithTag("KeybindController");
        if (keybindController != null)
        {
            keybindsMenu = keybindController.GetComponent<Keybind>();
            if (keybindsMenu == null)
            {
                Debug.Log("Cannot find 'Keybind' script");
            }
        }
        else
        {
            Debug.Log("Cannot find 'KeybindController' object");
        }

        p1MoveLeft = keybindsMenu.GetP1LeftKey();
        p1MoveRight = keybindsMenu.GetP1RightKey();
        p2MoveLeft = keybindsMenu.GetP2LeftKey();
        p2MoveRight = keybindsMenu.GetP2RightKey();
        UpdateSensibility1(keybindsMenu.p1Sensitivity);
        UpdateSensibility2(keybindsMenu.p2Sensitivity);
    }

    void FixedUpdate () {
        NetworkIdentity ni = gameObject.GetComponent<NetworkIdentity>();
        if (ni != null)
        {
            if (isLocalPlayer)
            {
                move();
            }
        }
        else
        {
            move();
        }
	}

    public void moveRight(float sensibility = 4000.0f, float distance = 0.50f)
    {
        moveHorizontal = distance;
        makeMovement(sensibility);
    }

    public void moveLeft(float sensibility = 4000.0f, float distance = -0.50f)
    {
        moveHorizontal = distance;
        makeMovement(sensibility);
    }

    public void moveNeutral()
    {
        moveHorizontal = 0.0f;
        makeMovement();
    }

    private void move()
    {
        if (playerOne)
        {
            if (Input.GetKey(p1MoveRight))
            {
                moveRight(sensibility1);
            }
            else if (Input.GetKey(p1MoveLeft))
            {
                moveLeft(sensibility1);
            }
            else
            {
                moveNeutral();
            }
        }
        else
        {
            if (!ai)
            {
                if (Input.GetKey(p2MoveRight))
                {
                    moveRight(sensibility2);
                }
                else if (Input.GetKey(p2MoveLeft))
                {
                    moveLeft(sensibility2);
                }
                else
                {
                    moveNeutral();
                }
            }
        }
    }

    private void makeMovement(float sensibility = 4000.0f)
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, -moveHorizontal);

        GetComponent<Rigidbody>().velocity = movement * sensibility;

        GetComponent<Rigidbody>().position = new Vector3
        (
            -1500.0f,
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, bottomBoardBoundary, topBoardBoundary)
        );
    }

    public void setStickOptionsForOnline(float topBoardBoundary, float bottomBoardBoundary, bool playerOne, bool ai)
    {
        this.topBoardBoundary = topBoardBoundary;
        this.bottomBoardBoundary = bottomBoardBoundary;
        this.playerOne = playerOne;
        this.ai = ai;
    }

    public void destroy()
    {
        Destroy(gameObject);
        Destroy(this);
    }
}
