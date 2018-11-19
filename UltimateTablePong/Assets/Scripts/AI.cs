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

    // Use this for initialization
    void Start() {
        UnpauseAI();
        ballsOnField = false;

        //Remove after testing
        difficulty = "easy"; // Set to easy for testing purposes

        stick = GameObject.Find("Stick2").GetComponent<Stick>();

        functionList = new List<System.Action>();
        functionList.Add(() => stick.moveLeft());
        functionList.Add(() => stick.moveRight());
        functionList.Add(() => stick.moveNeutral());
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

    public void setSimulation(string difficulty) {
        this.difficulty = difficulty;
    }

    void setCurrentPuck()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length != 0)
        {
            ballsOnField = true;
            this.puck = findLowestBall(balls);
        }
        else
        {
            ballsOnField = false;
        }
        Debug.Log(balls.Length);
    }

    Puck findLowestBall(GameObject[] balls)
    {
        GameObject lowestBall = balls[0];
        foreach (GameObject ball in balls)
        {
            if (ball.GetComponent<Puck>().transform.position.x < lowestBall.GetComponent<Puck>().transform.position.x)
            {
                lowestBall = ball;
            }
        }
        return lowestBall.GetComponent<Puck>();
    }

    void ApplyEasyAI()
    {
        int functionToExecute = Random.Range(0, functionList.Count);
        Debug.Log(functionToExecute);
        functionList[functionToExecute].Invoke();
    }

    void ApplyHardAI()
    {
        if ((puck.transform.position.z - stick.transform.position.z) < 0)
        {
            Debug.Log("Moving right");
            stick.moveRight();
        }
        if ((puck.transform.position.z - stick.transform.position.z) > 0)
        {
            Debug.Log("Moving left");
            stick.moveLeft();
        }
        else
        {
            Debug.Log("Under ball. Not moving");
            stick.moveNeutral();
        }
    }

    //private void returnToMiddle()
    //{
    //    float middlePosition = 254;
    //    while (stick.transform.position.z != middlePosition)
    //    {
    //        if (stick.transform.position.z < middlePosition)
    //        {
    //            stick.moveRight();
    //        }
    //        if (stick.transform.position.z > middlePosition)
    //        {
    //            stick.moveLeft();
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //}

    public void PauseAIAndReturnToMiddle()
    {
        //returnToMiddle();
        pause = true;
    }

    public void UnpauseAI()
    {
        pause = false;
    }
}
