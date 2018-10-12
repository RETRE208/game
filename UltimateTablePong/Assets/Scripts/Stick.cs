using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    private float speed;
    private float topBoardBoundary;
    private float bottomBoardBoundary;

    private void Start()
    {
        speed = 1500.0f;
        topBoardBoundary = 425.0f;
        bottomBoardBoundary = -425.0f;
    }

    void FixedUpdate () {
        move();
	}

    private void move()
    {
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);

        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            -450.0f,
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, bottomBoardBoundary, topBoardBoundary)
        );
    }


}
