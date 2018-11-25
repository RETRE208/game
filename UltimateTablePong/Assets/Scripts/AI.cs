using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    private string difficulty;
    private Puck puck;
    private Stick stick;
    private bool pause;
    private bool ballsOnField;
    private List<System.Action> functionList;
    private bool firstMove;

    // Use this for initialization
    void Start() {
        UnpauseAI();
        ballsOnField = false;
        firstMove = true;

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("Stick2");
        if (gameControllerObject != null)
        {
            stick = gameControllerObject.GetComponent<Stick>();
            if (stick == null)
            {
                Debug.Log("Cannot find 'Stick' script");
            }
        }
        else
        {
            Debug.Log("Cannot find 'Stick' object");
        }
    }

    // Update is called once per frame
    void Update() {
        setCurrentPuck();
        if (!pause && ballsOnField)
        {
            if (difficulty == "easy")
            {
                ApplyEasyAI();
            }
            else if (difficulty == "hard")
            {
                ApplyHardAI();
            }
        }
    }

    public void setSimulationEasy() {
        difficulty = "easy";
        stick.ai = true;
    }

    public void setSimulationHard()
    {
        difficulty = "hard";
        stick.ai = true;
    }

    public void removeAi()
    {

        difficulty = "";
        stick.ai = false;
    }

    void setCurrentPuck()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length != 0)
        {
            this.puck = findLowestBall(balls);
        }
        else
        {
            ballsOnField = false;
            PauseAIAndReturnToMiddle();
        }
    }

    Puck findLowestBall(GameObject[] balls)
    {
        GameObject lowestBall = balls[0];
        foreach (GameObject ball in balls)
        {
            if (ball.GetComponent<Puck>().transform.position.z < -2000)
            {
                ballsOnField = true;
                if (pause)
                {
                    UnpauseAI();
                }
                if (ball.GetComponent<Puck>().transform.position.x < lowestBall.GetComponent<Puck>().transform.position.x)
                {
                    lowestBall = ball;
                }
            }
        }
        return lowestBall.GetComponent<Puck>();
    }

    void ApplyEasyAI()
    {
        Debug.Log(stick.transform.position.z);
        if (firstMove)
        {
            stick.moveLeft();
            firstMove = false;
        }
        else if (stick.transform.position.z < -8624)
        {
            stick.moveLeft();
        }
        else if (stick.transform.position.z > -6987)
        {
            stick.moveRight();
        }
    }

    void ApplyHardAI()
    {
        if (puck.transform.position.z < (stick.transform.position.z + 150) && puck.transform.position.z > (stick.transform.position.z - 150))
        {
            stick.moveNeutral();
        }
        else if ((puck.transform.position.z - stick.transform.position.z) < 0)
        {
            Debug.Log("Right");
            stick.moveRight();
        }
        else if ((puck.transform.position.z - stick.transform.position.z) > 0)
        {
            Debug.Log("left");
            stick.moveLeft();
        }
        else
        {
            Debug.Log("stop");
            stick.moveNeutral();
        }
    }

    public void PauseAIAndReturnToMiddle()
    {
        stick.moveNeutral();
        pause = true;
    }

    public void UnpauseAI()
    {
        pause = false;
        firstMove = true;
    }
}
