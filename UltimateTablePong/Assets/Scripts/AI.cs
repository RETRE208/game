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
    private bool activated;

    // Use this for initialization
    void Start() {
        activated = false;
        pause = true;
        ballsOnField = false;
        firstMove = true;
    }

    // Update is called once per frame
    void Update() {
        if (activated)
        {
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
        activated = false;
        pause = true;
        difficulty = "";
        stick = null;
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
            stick.moveRight();
        }
        else if ((puck.transform.position.z - stick.transform.position.z) > 0)
        {
            stick.moveLeft();
        }
        else
        {
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

    public void activateAI()
    {
        Debug.Log(stick);
        stick = GameObject.FindGameObjectWithTag("Stick2").GetComponent<Stick>();
        Debug.Log(stick);
        activated = true;
    }
}
