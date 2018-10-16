using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public Text playerReadyText;
    private int scorePlayer1;
    private int scorePlayer2;
    public int ballCount;
    public GameObject balls;
    public Vector3 spawnValues;
    public bool player1turn;
    public int numberOfRounds;
    private int currentRound;

    // Use this for initialization
    void Start () {
        currentRound = 1;
        player1turn = true;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        ballCount = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        UpdateScore();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerReadyText.gameObject.SetActive(false);
            SpawnBall();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
        scoreText.text = "Score Player 1 : " + scorePlayer1 + " Player 2 : " + scorePlayer2;
    }

    public void AddScorePlayer(int newScoreValue)
    {
        if (player1turn)
        {
            scorePlayer1 += newScoreValue;
        }
        else
        {
            scorePlayer2 += newScoreValue;
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
    }

    public void PuckIsDestroy(Puck puck)
    {
        ballCount -= 1;
        if(ballCount <= 0)
        {
            if (!player1turn)
            {
                currentRound += 1;
            }
            if(currentRound > numberOfRounds)
            {
                playerReadyText.text = GetWinner();
                playerReadyText.gameObject.SetActive(true);
            }
            else
            {
                ChangeSides();
            }
        }
    }

    private string GetWinner()
    {
        string winner;
        if (scorePlayer1 > scorePlayer2)
        {
            winner = "Félicitation\n Le joueur 1 gagne!";
        }
        else
        {
            winner = "Félicitation\n Le joueur 2 gagne!";
        }
        return winner;
    }
}
