using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Puck : MonoBehaviour {

    private Vector3 position;
    private Vector3 movement;
    private Vector3 gravity;
    private Rigidbody rb;
    private GameController gameController;
    public float speed;

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
        gravity = new Vector3(-50.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        addGravity();
        position = rb.position;
        rb.velocity = movement;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(0, 0, 1));
            gameController.PuckIsDestroy(this);
            Destroy(gameObject);
        }
        else if (other.tag == "LeftBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(0, 0, -1));
        }
        else if (other.tag == "Stick")
        {
            movement = getNewDirectionAfterCollision(new Vector3(0, 0, -1));
            gameController.AddScorePlayer(2);
            getStickBoost();
            gameController.hitStick();
        }
        else if (other.tag == "TopBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(1, 0, 0));
            gameController.AddScorePlayer(1);
        }
        else if (other.tag == "BottomBorder")
        {
            movement = getNewDirectionAfterCollision(new Vector3(-1, 0, 0));
            gameController.AddScorePlayer(1);
        }
        else if (other.tag == "Ball")
        {
            rb.position = position;
            Puck otherPuck = other.gameObject.GetComponent<Puck>();
            Vector3 normal = calculateNormal(otherPuck.getMovement());
            movement = getNewDirectionAfterCollision(normal);
        }
        else if (other.tag == "Obstacle")
        {
            gameController.AddScorePlayer(5);
            getNewDirectionAfterCollisionWithObstacle(other.gameObject.GetComponent<Rigidbody>().position, other.bounds.size.x);
        }
    }

    void moveRandomDirection()
    {
        float randomZ = Random.Range(1, 10);
        movement.x = 0;
        movement.y = 0;
        movement.z = 100*randomZ;
    }

    void getNewDirectionAfterCollisionWithObstacle(Vector3 obstaclePosition, float obstacleRay)
    {
        Vector3 intersection = ClosestIntersection(obstaclePosition.z, obstaclePosition.x, obstacleRay, position, rb.position);

        Vector3 normal;
        normal.x = intersection.x - obstaclePosition.x;
        normal.y = 0.0f;
        normal.z = intersection.z - obstaclePosition.z;
        normal.Normalize();

        rb.position = position;
        movement = getNewDirectionAfterCollision(normal);
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

    private void addGravity()
    {
        movement.x += gravity.x;
    }

    private void getStickBoost()
    {
        movement.x = 4500;
    }

    public Vector3 ClosestIntersection(float cx, float cy, float radius, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 intersection1;
        Vector3 intersection2;
        int intersections = FindLineCircleIntersections(cx, cy, radius, lineStart.z, lineStart.x, lineEnd.z, lineEnd.x, out intersection1, out intersection2);

        if (intersections == 1)
            return intersection1;//one intersection

        if (intersections == 2)
        {
            double dist1 = Distance(intersection1, lineStart);
            double dist2 = Distance(intersection2, lineStart);

            if (dist1 < dist2)
                return intersection1;
            else
                return intersection2;
        }

        return new Vector3(0,0,0);// no intersections at all
    }

    private double Distance(Vector3 p1, Vector3 p2)
    {
        return Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.z - p1.z, 2);
    }


    private int FindLineCircleIntersections(float cx, float cy, float radius, float point1X, float point1Y, float point2X, float point2Y, out Vector3 intersection1, out Vector3 intersection2)
    {
        float dx, dy, A, B, C, det, t;

        dx = point2X - point1X;
        dy = point2Y - point1Y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1X - cx) + dy * (point1Y - cy));
        C = (point1X - cx) * (point1X - cx) + (point1Y - cy) * (point1Y - cy) - radius * radius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            // No real solutions.
            intersection1 = new Vector3(float.NaN, float.NaN, float.NaN);
            intersection2 = new Vector3(float.NaN, float.NaN, float.NaN);
            return 0;
        }
        else if (det == 0)
        {
            // One solution.
            t = -B / (2 * A);
            intersection1 = new Vector3(point1X + t * dx, point1Y + t * dy);
            intersection2 = new Vector3(float.NaN, float.NaN, float.NaN);
            return 1;
        }
        else
        {
            // Two solutions.
            t = (float)((-B + Mathf.Sqrt(det)) / (2 * A));
            intersection1 = new Vector3(point1X + t * dx, 50.0f, point1Y + t * dy);
            t = (float)((-B - Mathf.Sqrt(det)) / (2 * A));
            intersection2 = new Vector3(point1X + t * dx, 50.0f, point1Y + t * dy);
            return 2;
        }
    }

}