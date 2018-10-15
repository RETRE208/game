using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Puck : MonoBehaviour {

    private Vector3 movement;
    private Rigidbody rb;
    private GameController gameController;
    public float speed;
    public bool player1Turn;

	// Use this for initialization
	void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            if (gameController == null)
            {
                Debug.Log("Cannot find 'GameController' script");
            }
        }
        else
        {
            Debug.Log("Cannot find 'GameController' object");
        }
        rb = GetComponent<Rigidbody>();
        moveRandomDirection();
        player1Turn = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player1Turn)
        {
            if (transform.position.x >= 2440)
            {
                rb.velocity = Vector3.zero;
                gameController.PuckIsReady();
            }
            else
            {
                rb.velocity = movement;
            }
        }
        else
        {
            if (transform.position.x <= 2440)
            {
                rb.velocity = Vector3.zero;
                gameController.PuckIsReady();
            }
            else
            {
                rb.velocity = movement;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(0, 0, 1));
        }
        else if (other.tag == "LeftBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(0, 0, -1));
        }
        else if (other.tag == "Stick")
        {
            movement = getNewDirectionAfterCollision(new Vector3(0, 0, -1));
        }
        else if (other.tag == "TopBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(1, 0, 0));
        }
        else if (other.tag == "BottomBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(-1, 0, 0));
        }
        else if (other.tag == "Puck")
        {
            Puck otherPuck = other.gameObject.GetComponent<Puck>();
            Vector3 normal = calculateNormal(otherPuck.getMovement());
            movement = getNewDirectionAfterCollision(normal);
        }
        else if (other.tag == "PathWall")
        {
            getNewDirectionAfterCollisionWithPathWall(other.gameObject.GetComponent<Rigidbody>().position, other.bounds.size);
        }
    }

    void moveRandomDirection()
    {
        float randomX = Random.Range(1, 10);
        float valueZ = Mathf.Sqrt(speed - (randomX * randomX));
        movement.x = 100*randomX;
        movement.y = 0;
        movement.z = 100* valueZ;
    }

    void getNewDirectionAfterCollisionWithPathWall(Vector3 pathWallPosition, Vector3 pathWallSize)
    {
        float pathWallLeftPosition = pathWallPosition.x - (pathWallSize.x / 2) - 5;
        float pathWallRightPosition = pathWallPosition.x + (pathWallSize.x / 2) - 5;
        float pathWallTopPosition = pathWallPosition.z + (pathWallSize.z / 2) - 5;
        float pathWallBottomPosition = pathWallPosition.z - (pathWallSize.z / 2) - 5;

        if (rb.position.x > pathWallLeftPosition && rb.position.x < pathWallRightPosition)
        {
            string s = "Puck : " + rb.position + "left : " + pathWallLeftPosition + "    Right : " + pathWallRightPosition + "  Bottom : " + pathWallBottomPosition + "    Top : " + pathWallTopPosition;
            Debug.Log(s);
            if (rb.position.z > pathWallPosition.z)
            {
                movement = getNewDirectionAfterCollision(new Vector3(1, 0, 0));
            }
            else
            {
                movement = getNewDirectionAfterCollision(new Vector3(-1, 0, 0));
            }
        }
        else if (rb.position.z > pathWallBottomPosition && rb.position.z < pathWallTopPosition)
        {
            if (rb.position.x > pathWallPosition.x)
            {
                movement = getNewDirectionAfterCollision(new Vector3(0, 0, 1));
            }
            else
            {
                movement = getNewDirectionAfterCollision(new Vector3(0, 0, -1));
            }
        }
    }

    Vector3 getNewDirectionAfterCollision(Vector3 collisionNormal)
    {
        Vector3 newDirection;

        float dotProduct = ((movement.x * collisionNormal.x)
                        + (movement.y * collisionNormal.y)
                        + (movement.z * collisionNormal.z));

        newDirection.x = (2 * dotProduct * collisionNormal.x) - movement.x;
        newDirection.y = (2 * dotProduct * collisionNormal.y) - movement.y;
        newDirection.z = (2 * dotProduct * collisionNormal.z) - movement.z;

        return newDirection;
    }

    Vector3 calculateNormal(Vector3 otherMovement)
    {
        Vector3 normal;

        normal.x = 0;
        normal.y = (movement.z * otherMovement.x) - (movement.x * otherMovement.z);
        normal.z = 0;

        return normal;
    }

    public Vector3 getMovement()
    {
        return movement;
    }
}
