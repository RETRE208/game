using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    private float speed;
    public float topBoardBoundary;
    public float bottomBoardBoundary;

    private void Start()
    {
        speed = 2500.0f;
    }

    void FixedUpdate () {
        move();
	}

    private void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

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
