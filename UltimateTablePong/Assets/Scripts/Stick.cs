using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    private float speed;
    public float topBoardBoundary;
    public float bottomBoardBoundary;
    private float moveHorizontal;

    private void Start()
    {
        speed = 2500.0f;
        moveHorizontal = 0.0f;
    }

    void FixedUpdate () {
        move();
	}

    private void move()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 0.30f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -0.30f;
        }
        else
        {
            moveHorizontal = 0.0f;
        }

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
