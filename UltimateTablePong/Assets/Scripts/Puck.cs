using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Puck : MonoBehaviour {

    private Vector3 position;
    private Vector3 direction;
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
        rb.velocity = direction;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightBorder")
        {
            gameController.PuckIsDestroy(this);
            Destroy(gameObject);
        }
        else if (other.tag == "LeftBorder")
        {
            direction = getNewDirectionAfterCollision(new Vector3(0, 0, 1));
        }
        else if (other.tag == "Stick" || other.tag == "Stick2")
        {
            direction = getNewDirectionAfterCollision(new Vector3(0, 0, -1));
            gameController.AddScorePlayer(2);
            getStickBoost();
            gameController.hitStick();
        }
        else if (other.tag == "TopBorder")
        {
            direction = getNewDirectionAfterCollision(new Vector3(1, 0, 0));
            gameController.AddScorePlayer(1);
        }
        else if (other.tag == "BottomBorder")
        {
            direction = getNewDirectionAfterCollision(new Vector3(-1, 0, 0));
            gameController.AddScorePlayer(1);
        }
        else if (other.tag == "Ball")
        {
            rb.position = position;
            Puck otherPuck = other.gameObject.GetComponent<Puck>();
            Vector3 normal = calculateNormal(otherPuck.getDirection());
            direction = getNewDirectionAfterCollision(normal);
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
        int signZ = 1; //Disabled randomness for the simulation (Random.value > 0.5f) ? 1 : -1;
        direction.x = 0;
        direction.y = 0;
        direction.z = 100 * randomZ * signZ;
    }

    void getNewDirectionAfterCollisionWithObstacle(Vector3 obstaclePosition, float obstacleRay)
    {
        Vector3 intersection = getImpactPoint(obstaclePosition.z, obstaclePosition.x, obstacleRay, position, rb.position);

        Vector3 normal;
        normal.x = intersection.x - obstaclePosition.x;
        normal.y = 0.0f;
        normal.z = intersection.z - obstaclePosition.z;
        normal.Normalize();

        rb.position = position;
        direction = getNewDirectionAfterCollision(normal);
    }

    Vector3 getNewDirectionAfterCollision(Vector3 collisionNormal)
    {
        Vector3 newDirection;

        float dotProduct = ((direction.x * collisionNormal.x)
                        + (direction.y * collisionNormal.y)
                        + (direction.z * collisionNormal.z));

        newDirection.x = (2 * dotProduct * collisionNormal.x) - direction.x;
        newDirection.y = (2 * dotProduct * collisionNormal.y) - direction.y;
        newDirection.z = (2 * dotProduct * collisionNormal.z) - direction.z;

        return newDirection;
    }

    Vector3 calculateNormal(Vector3 otherDirection)
    {
        Vector3 normal;

        normal.x = 0;
        normal.y = (direction.z * otherDirection.x) - (direction.x * otherDirection.z);
        normal.z = 0;

        return normal;
    }

    public Vector3 getDirection()
    {
        return direction;
    }

    private void addGravity()
    {
        direction.x += gravity.x;
    }

    private void getStickBoost()
    {
        direction.x = 4000;
    }

    public Vector3 getImpactPoint(float circleCenterX, float circleCenterY, float circleRadius, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 intersection1;
        Vector3 intersection2;
        int numberIntersections = FindLineCircleIntersections(circleCenterX, circleCenterY, circleRadius, lineStart.z, lineStart.x, lineEnd.z, lineEnd.x, out intersection1, out intersection2);

        if (numberIntersections == 1)
            //Tangeant point, the ball direction is not modified.
            return intersection1;

        if (numberIntersections == 2)
        {
            double dist1 = getDistanceBetween2Points(intersection1, lineStart);
            double dist2 = getDistanceBetween2Points(intersection2, lineStart);

            if (dist1 < dist2)
                return intersection1;
            else
                return intersection2;
        }

        // Should never come here. If the event is triggered, there is an impact point.
        return new Vector3(0,0,0);
    }

    private double getDistanceBetween2Points(Vector3 p1, Vector3 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.z - p1.z, 2));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // The code to get the intersection of a circle with a line was found here : 
    // http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
    ////////////////////////////////////////////////////////////////////////////////////////////////
    private int FindLineCircleIntersections(float circleCentreX, float circleCentreY, float circleRadius, 
                                            float point1X, float point1Y, float point2X, float point2Y, 
                                            out Vector3 intersection1, out Vector3 intersection2)
    {
        float deltaX, deltaY;
        float A, B, C;
        float det, t;

        deltaX = point2X - point1X;
        deltaY = point2Y - point1Y;

        A = deltaX * deltaX + deltaY * deltaY;
        B = 2 * (deltaX * (point1X - circleCentreX) + deltaY * (point1Y - circleCentreY));
        C = (point1X - circleCentreX) * (point1X - circleCentreX) + (point1Y - circleCentreY) * (point1Y - circleCentreY) - circleRadius * circleRadius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            intersection1 = new Vector3(float.NaN, float.NaN, float.NaN);
            intersection2 = new Vector3(float.NaN, float.NaN, float.NaN);
            return 0;
        }
        else if (det == 0)
        {
            t = -B / (2 * A);
            intersection1 = new Vector3(point1X + t * deltaX, point1Y + t * deltaY);
            intersection2 = new Vector3(float.NaN, float.NaN, float.NaN);
            return 1;
        }
        else
        {
            t = (float)((-B + Mathf.Sqrt(det)) / (2 * A));
            intersection1 = new Vector3(point1X + t * deltaX, 50.0f, point1Y + t * deltaY);
            t = (float)((-B - Mathf.Sqrt(det)) / (2 * A));
            intersection2 = new Vector3(point1X + t * deltaX, 50.0f, point1Y + t * deltaY);
            return 2;
        }
    }

}