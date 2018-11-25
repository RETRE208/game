using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    private float speed;
    public float topBoardBoundary;
    public float bottomBoardBoundary;
    public bool playerOne;
    public bool ai;
    private float moveHorizontal;

    private Keybind keybindsMenu;
    private KeyCode p1MoveLeft;
    private KeyCode p1MoveRight;
    private KeyCode p2MoveLeft;
    private KeyCode p2MoveRight;

    private void Start()
    {
        speed = 4000.0f;
        moveHorizontal = 0.0f;
        ai = true;
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
    }

    public void UpdateControls()
    {
        p1MoveLeft = keybindsMenu.GetP1LeftKey();
        p1MoveRight = keybindsMenu.GetP1RightKey();
        p2MoveLeft = keybindsMenu.GetP2LeftKey();
        p2MoveRight = keybindsMenu.GetP2RightKey();
    }

    void FixedUpdate () {
        move();
	}

    public void moveRight(float distance = 0.50f)
    {
        moveHorizontal = distance;
        makeMovement();
    }

    public void moveLeft(float distance = -0.50f)
    {
        moveHorizontal = distance;
        makeMovement();
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
                moveRight();
            }
            else if (Input.GetKey(p1MoveLeft))
            {
                moveLeft();
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
                    moveRight();
                }
                else if (Input.GetKey(p2MoveLeft))
                {
                    moveLeft();
                }
                else
                {
                    moveNeutral();
                }
            }
        }
    }

    private void makeMovement()
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, -moveHorizontal);

        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            -1500.0f,
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, bottomBoardBoundary, topBoardBoundary)
        );
    }
}
