using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public Text playerReadyText;
    public Text gameInfo;
    private int scorePlayer1;
    private int scorePlayer2;
    private int ballCount;
    public GameObject balls;
    public Vector3 spawnValues;
    private bool player1turn;
    public int numberOfRounds;
    private int currentRound;
    private int numberOfHit;

    private Stick stick;
    private EndMenu endMenu;
    private PauseMenu pauseMenu;

    // Use this for initialization
    void Start () {
        pauseMenu = FindObjectOfType<PauseMenu>();
        endMenu = FindObjectOfType<EndMenu>();

        currentRound = 1;
        player1turn = true;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        ballCount = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        UpdateScore();
        numberOfHit = 0;
        getGameInfo();
        

        pauseMenu.SetRestartGameAction(restartGame);
        endMenu.SetRestartGameAction(restartGame);

        stick = GameObject.Find("Stick").GetComponent<Stick>();
    }

    void FixedUpdate()
    {
        getGameInfo();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerReadyText.gameObject.SetActive(false);
            SpawnBall();
        }
    }

    public void SetNumberOfRounds(int numberOfRounds)
    {
        this.numberOfRounds = numberOfRounds;
    }

    void SpawnBall()
    {
        ballCount += 1;
        GameObject ball = balls;
        Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(ball, spawnPosition, spawnRotation);
    }

    void UpdateScore () {
        scoreText.text = "Score \nPlayer 1 : " + scorePlayer1 + " \nPlayer 2 : " + scorePlayer2;
    }

    public void AddScorePlayer(int newScoreValue)
    {
        if (player1turn)
        {
            scorePlayer1 += (newScoreValue * ballCount);
        }
        else
        {
            scorePlayer2 += (newScoreValue * ballCount);
        }
        
        UpdateScore();
    }

    void ChangeSides()
    {

        if (player1turn)
        {
            player1turn = false;
        }
        else
        {
            player1turn = true;
        }

        if (player1turn)
        {
            playerReadyText.text = "Player one's turn, \n Press space to start!";
        }
        else
        {
            playerReadyText.text = "Player two's turn, \n Press space to start!";
        }
        playerReadyText.gameObject.SetActive(true);
        getGameInfo();
        numberOfHit = 0;
    }

    public void PuckIsDestroy(Puck puck)
    {
        ballCount -= 1;
        numberOfHit = 0;
        if (ballCount <= 0)
        {
            if (!player1turn)
            {
                currentRound += 1;
            }
            if(currentRound > numberOfRounds)
            {
                GetWinner();
            }
            else
            {
                ChangeSides();
            }
        }
    }

    private void GetWinner()
    {
        string winner;
        if (scorePlayer1 > scorePlayer2)
        {
            winner = "Congratulation! \n Player one win with " + scorePlayer1 + "points";
        }
        else if (scorePlayer2 > scorePlayer1)
        {
            winner = "Congratulation! \n Player two win with " + scorePlayer2 + "points";
        }
        else
        {
            winner = " DRAW ";
        }

        
        endMenu.DisplayEndMenu(winner);
    }

    public void hitStick()
    {
        numberOfHit += 1;
        if (ballCount == 1 && numberOfHit == 4)
        {
            SpawnBall();
            numberOfHit = 0;
        }
        if (ballCount == 2 && numberOfHit == 9)
        {
            SpawnBall();
            numberOfHit = 0;
        }
    }

    private void restartGame()
    {
        endMenu.HideEndMenu();
        destroyAllBalls();
        stick.transform.position = new Vector3(-1500, 0, 42);

        player1turn = false;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        currentRound = 1;
        numberOfHit = 0;
        ballCount = 0;
        getGameInfo();
        UpdateScore();
        ChangeSides();
    }

    private void getGameInfo()
    {
        if (player1turn)
        {
            gameInfo.text = "Player one's turn\n";
        }
        else
        {
            gameInfo.text = "Player two's turn\n";
        }
        gameInfo.text += "Round " + currentRound + "/" + numberOfRounds;
    }

    private void destroyAllBalls()
    {
        GameObject[]  balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }
}
