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
    private KeyCode startButton;

    private EndMenu endMenu;
    private PauseMenu pauseMenu;
    private Keybind keybindsMenu;

    private bool isOnlineMode;
    private bool isHost;
    private bool isPlayer2Connected;
    private BallSpawner ballSpawner;

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

        isOnlineMode = false;
        isHost = true;
        isPlayer2Connected = false;

        pauseMenu.SetRestartGameAction(restartGame);
        endMenu.SetRestartGameAction(restartGame);

        GameObject keybindController = GameObject.FindGameObjectWithTag("KeybindController");
        if (keybindController != null)
        {
            keybindsMenu = keybindController.GetComponent<Keybind>();
        }
    }

    public void UpdateStartButton()
    {
        startButton = keybindsMenu.GetStartButton();
    }

    void FixedUpdate()
    {
        if (playerReadyText.gameObject.activeSelf && isOnlineMode && !isHost)
        {
            playerReadyText.gameObject.SetActive(false);
        }
        getGameInfo();
        if (!isOnlineMode)
        {
            if (Input.GetKeyDown(startButton))
            {
                if (playerReadyText.gameObject.activeSelf)
                {
                    playerReadyText.gameObject.SetActive(false);
                    SpawnBall();
                }
            }
        } else
        {
            if (isPlayer2Connected)
            {
                if (Input.GetKeyDown(startButton))
                {
                    if (playerReadyText.gameObject.activeSelf)
                    {
                        playerReadyText.gameObject.SetActive(false);
                        SpawnBall();
                    }
                }
            }
        }

    }

    private GameObject[] FindAllBalls()
    {
        return GameObject.FindGameObjectsWithTag("Ball");
    }

    public void SetNumberOfRounds(int numberOfRounds)
    {
        this.numberOfRounds = numberOfRounds;
    }

    void SpawnBall()
    {
        GameObject ball = balls;
        Vector3 spawnPosition;
        if (player1turn)
        {
            spawnPosition = new Vector3(0.0f, 50.0f, 0.0f);
        }
        else
        {
            spawnPosition = new Vector3(0.0f, 50.0f, -8000.0f);
        }
        Quaternion spawnRotation = Quaternion.identity;
        if (isOnlineMode && isHost)
        {
            ballSpawner = GameObject.FindObjectOfType<BallSpawner>();
            ballSpawner.SpawnBall();
            ballCount += 2;
        } else if (isOnlineMode && !isHost)
        {
            
        }
        else {
            Instantiate(ball, spawnPosition, spawnRotation);
            ballCount += 1;
        }
    }

    void UpdateScore () {
        scoreText.text = "Score \nPlayer 1 : " + scorePlayer1 + " \nPlayer 2 : " + scorePlayer2;
    }

    public void AddScorePlayer(int newScoreValue, int player)
    {
        if (isOnlineMode)
        {
            if (player == 1)
            {
                scorePlayer1 += (newScoreValue * ballCount);
            } else
            {
                scorePlayer2 += (newScoreValue * ballCount);
            }
        }
        else
        {
            if (player1turn)
            {
                scorePlayer1 += (newScoreValue * ballCount);
            }
            else
            {
                scorePlayer2 += (newScoreValue * ballCount);
            }
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
            playerReadyText.text = "Player one's turn, \n ";
        }
        else
        {
            playerReadyText.text = "Player two's turn, \n ";
        }
        playerReadyText.text += "Press the start button(" + startButton.ToString() + ") to start!";
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
        if (!isOnlineMode)
        {
            numberOfHit += 1;
            if (ballCount == 1 && numberOfHit == 3)
            {
                SpawnBall();
                numberOfHit = 0;
            }
            if (ballCount == 2 && numberOfHit == 5)
            {
                SpawnBall();
                numberOfHit = 0;
            }
        }
        else
        {
            numberOfHit += 1;
            if (numberOfHit == 4)
            {
                SpawnBall();
                numberOfHit = 0;
            }
        }
    }

    private void restartGame()
    {
        endMenu.HideEndMenu();
        destroyAllBalls();

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
        if (!isOnlineMode)
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
        else
        {
            if (player1turn)
            {
                gameInfo.text = "Round 1/2\n";
            }
            else
            {
                gameInfo.text = "Round 2/2\n";
            }
        }
    }

    private void destroyAllBalls()
    {
        GameObject[]  balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }


    public void setOnlineMode(bool onlineMode, bool host)
    {
        this.isOnlineMode = onlineMode;
        this.isHost = host;

        if (!host)
        {
            playerReadyText.gameObject.SetActive(false);
        }
    }

    public void player2Connected(bool isPlayer2Connected)
    {
        this.isPlayer2Connected = isPlayer2Connected;
    }
}
