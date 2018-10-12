using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Puck : MonoBehaviour {

    private Vector3 movement;
    private Rigidbody rb;

    public float speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        moveRandomDirection();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = movement;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(0,0,1));
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
    }

    void moveRandomDirection()
    {
        float randomX = Random.Range(1, 10);
        float valueZ = Mathf.Sqrt(speed - (randomX * randomX));
        movement.x = 100*randomX;
        movement.y = 0;
        movement.z = 100* valueZ;
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
