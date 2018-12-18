using System.Collections.Generic;
using UnityEngine;

public class ParticuleController : MonoBehaviour {

    private static readonly int OBSTACLE_PARTICULES_IN_POOL = 10;
    private static readonly int STICK_PARTICULES_IN_POOL = 3;
    private static readonly int BALL_PARTICULES_IN_POOL = 2;

    public GameObject obstacleCollisionParticulePrefab;
    public GameObject stickCollisionParticulePrefab;
    public GameObject ballCollisionParticulePrefab;

    public GameObject obstacleLight;
    public GameObject stickLight;
    public GameObject ballLight;

    private List<GameObject> obstacleCollisionParticles;
    private List<GameObject> stickCollisionParticles;
    private List<GameObject> ballCollisionParticles;

    private int currentObstacleParticle;
    private int currentStickParticle;
    private int currentBallParticle;

    void Start () {
        obstacleCollisionParticles = new List<GameObject>();
        stickCollisionParticles = new List<GameObject>();
        ballCollisionParticles = new List<GameObject>();

        InstanciatePools();

        currentObstacleParticle = 0;
        currentStickParticle = 0;
        currentBallParticle = 0;
    }

    public void ObstacleCollision(Vector3 collisionPosition)
    {
        if (currentObstacleParticle >= OBSTACLE_PARTICULES_IN_POOL) currentObstacleParticle = 0;

        ParticleSystem.ShapeModule shape = obstacleCollisionParticles[currentObstacleParticle].GetComponent<ParticleSystem>().shape;
        shape.position = collisionPosition;
        obstacleCollisionParticles[currentObstacleParticle].GetComponent<ParticleSystem>().Play();

        InstanciateObstacleLightEffect(collisionPosition);

        currentObstacleParticle++;
    }

    public void StickCollision(Vector3 collisionPosition)
    {
        if (currentStickParticle >= STICK_PARTICULES_IN_POOL) currentStickParticle = 0;

        ParticleSystem.ShapeModule shape = stickCollisionParticles[currentStickParticle].GetComponent<ParticleSystem>().shape;
        shape.position = collisionPosition;
        stickCollisionParticles[currentStickParticle].GetComponent<ParticleSystem>().Play();

        InstanciateStickLightEffect(collisionPosition);

        currentStickParticle++;
    }

    public void BallCollision(Vector3 collisionPosition)
    {
        if (currentBallParticle >= BALL_PARTICULES_IN_POOL) currentBallParticle = 0;

        ParticleSystem.ShapeModule shape = ballCollisionParticles[currentBallParticle].GetComponent<ParticleSystem>().shape;
        shape.position = collisionPosition;
        ballCollisionParticles[currentBallParticle].GetComponent<ParticleSystem>().Play();

        InstanciateBallLightEffect(collisionPosition);

        currentBallParticle++;
    }

    private void InstanciatePools()
    {
        for (int i = 0; i < OBSTACLE_PARTICULES_IN_POOL; i++)
        {
            GameObject particule = Instantiate(obstacleCollisionParticulePrefab, new Vector3(), new Quaternion());
            obstacleCollisionParticles.Add(particule);
        }
        for (int i = 0; i < STICK_PARTICULES_IN_POOL; i++)
        {
            GameObject particule = Instantiate(stickCollisionParticulePrefab, new Vector3(), new Quaternion());
            stickCollisionParticles.Add(particule);
        }
        for (int i = 0; i < BALL_PARTICULES_IN_POOL; i++)
        {
            GameObject particule = Instantiate(ballCollisionParticulePrefab, new Vector3(), new Quaternion());
            ballCollisionParticles.Add(particule);
        }
    }

    private void InstanciateObstacleLightEffect(Vector3 position)
    {
        Instantiate(obstacleLight, position, new Quaternion());
    }

    private void InstanciateStickLightEffect(Vector3 position)
    {
        Instantiate(stickLight, position, new Quaternion());
    }

    private void InstanciateBallLightEffect(Vector3 position)
    {
        Instantiate(ballLight, position, new Quaternion());
    }
}
