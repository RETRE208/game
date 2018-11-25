﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject slider1;
    private GameObject slider2;

    private float sensibility1;
    private float sensibility2;

    private void Start()
    {
        speed = 4000.0f;
        moveHorizontal = 0.0f;
        slider1 = GameObject.Find("Slider1");
        slider2 = GameObject.Find("Slider2");

        UpdateSensibility1();
        UpdateSensibility2();
    }

    private void UpdateSensibility1()
    {
        float speed1 = slider1.GetComponent<Slider>().value;
        speed1 = (speed1 * 1000) + 3000;
        sensibility1 = speed1;
    }

    private void UpdateSensibility2()
    {
        float speed2 = slider2.GetComponent<Slider>().value;
        speed2 = (speed2 * 1000) + 3000;
        sensibility2 = speed2;
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
    }

    void FixedUpdate () {
        move();
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
