﻿using System;
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
    private Avatars avatars;

    private bool isOnlineMode;
    private bool isHost;
    private bool isPlayer2Connected;
    private BallSpawner ballSpawner;

    private SoundManager soundManager;

    public bool aiMode;

    public GameObject stickPrefab;

    // Use this for initialization
    void Start () {
        soundManager = FindObjectOfType<SoundManager>();
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
        aiMode = false;

        pauseMenu.SetRestartGameAction(restartGame);
        pauseMenu.SetLeaveGameAction(LeaveGame);
        endMenu.SetRestartGameAction(restartGame);
        endMenu.SetLeaveGameAction(LeaveGame);

        GameObject keybindController = GameObject.FindGameObjectWithTag("KeybindController");
        if (keybindController != null)
        {
            keybindsMenu = keybindController.GetComponent<Keybind>();
        }

        GameObject avatarModifier = GameObject.FindGameObjectWithTag("Avatars");
        if (avatarModifier != null)
        {
            avatars = avatarModifier.GetComponent<Avatars>();
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
        ball.AddComponent<AudioSource>();
        ball.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("wallHit");
        ball.GetComponent<AudioSource>().playOnAwake = false;
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
                soundManager.changeMusicPitch(1F);
                GetWinner();
            }
            else
            {
                soundManager.changeMusicPitch(1F);
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
                soundManager.changeMusicPitch(1.1f);
                SpawnBall();
                numberOfHit = 0;
            }
            if (ballCount == 2 && numberOfHit == 5)
            {
                soundManager.changeMusicPitch(1.2f);
                SpawnBall();
                numberOfHit = 0;
            }
        }
        else
        {
            numberOfHit += 1;
            if (numberOfHit == 4)
            {
                soundManager.changeMusicPitch(1.1f);
                SpawnBall();
                numberOfHit = 0;
            }
        }
    }

    private void LeaveGame()
    {
        if (isOnlineMode)
        {
            NetworkManagerCustom manager = GameObject.FindObjectOfType<NetworkManagerCustom>();
            if (isHost)
            {
                manager.StopServer();
            }
            else
            {
                manager.OnStopClient();
            }

            createPlayersSticks();

            setOnlineMode(false, true);
            MainMenu mainMenu = GameObject.FindObjectOfType<MainMenu>();
            mainMenu.DisplayMainMenu();
        }
        if (aiMode)
        {
            AI aI = GameObject.FindGameObjectWithTag("Board").GetComponent<AI>();
            aI.removeAi();
            aiMode = false;
        }

        restartGame();
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

    public void DestroyAllSticks()
    {
        GameObject[] sticks = GameObject.FindGameObjectsWithTag("Stick");
        foreach (GameObject stick in sticks)
        {
            stick.GetComponent<Stick>().destroy();
        }
        sticks = GameObject.FindGameObjectsWithTag("Stick2");
        foreach (GameObject stick in sticks)
        {
            stick.GetComponent<Stick>().destroy();
        }
        sticks = GameObject.FindGameObjectsWithTag("StickOnline");
        foreach (GameObject stick in sticks)
        {
            stick.GetComponent<Stick>().destroy();
        }
    }

    public void createPlayersSticks()
    {
        string model1;
        GameObject prefab1;
        Color[] colors1;
        float leftLimit1 = 775.0f;
        float rightLimit1 = -775.0f;
        float size1;

        string model2;
        GameObject prefab2;
        Color[] colors2;
        float leftLimit2 = -7050.0f;
        float rightLimit2 = -8600.0f;
        float size2;

        Vector3 stickSize;

        DestroyAllSticks();

        model1 = avatars.getPlayer1Avatar(out prefab1, out colors1, out size1);

        leftLimit1 = 750.0f + ((size1 - 300) * -0.576f);
        rightLimit1 = -740.0f + ((size1 - 300) * 0.456f);

        /*if (model1.Equals("Flower"))
        {
            leftLimit1 = 775.0f + ((size1 - 300) * -0.576f);
            rightLimit1 = -775.0f + ((size1 - 300) * 0.456f);
        }
        else if (model1.Equals("Sword"))
        {
            leftLimit1 = 775.0f + ((size1 - 300) * -0.576f);
            rightLimit1 = -775.0f + ((size1 - 300) * 0.456f);
        }
        else if (model1.Equals("Normal"))
        {
            leftLimit1 = 775.0f + ((size1 - 300) * -0.576f);
            rightLimit1 = -775.0f + ((size1 - 300) * 0.456f);
        }*/
        GameObject stick1 = Instantiate(prefab1, new Vector3(), new Quaternion());
        stick1.GetComponent<Rigidbody>().position = new Vector3(-1500.0f, 0.0f, 42.0f);
        stick1.tag = "Stick";

        stickSize = stick1.GetComponent<Transform>().localScale;
        stickSize.z = size1;
        stick1.GetComponent<Transform>().localScale = stickSize;

        stick1.AddComponent<AudioSource>();
        stick1.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("palletHit");
        stick1.GetComponent<AudioSource>().playOnAwake = false;
        if (model1.Equals("Flower"))
        {
            var children = stick1.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.name.Equals("Stem"))
                {
                    if (!colors1[0].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors1[0];
                    }
                }
                else if (child.name.Equals("Middle"))
                {
                    if (!colors1[1].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors1[1];
                    }
                }
                else if (child.name.Contains("Petal"))
                {
                    if (!colors1[2].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors1[2];
                    }
                }
            }
        }
        if (model1.Equals("Sword"))
        {
            var children = stick1.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.name == "Blade")
                {
                    if (!colors1[0].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors1[0];
                    }
                }
                else if (child.name == "Crossguard")
                {
                    if (!colors1[1].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors1[1];
                    }
                }
                else if (child.name == "Grip")
                {
                    if (!colors1[2].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors1[2];
                    }
                }
                else if (child.name == "Pomel")
                {
                    if (!colors1[3].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors1[3];
                    }
                }
            }
        }
        if (model1.Equals("Normal"))
        {
            if (!colors1[0].Equals(Color.clear))
            {
                stick1.GetComponent<Renderer>().material.color = colors1[0];
            }
        }

        Stick stick1Script = stick1.GetComponent<Stick>();
        stick1Script.setStickOptions(leftLimit1, rightLimit1, true);
        stick1Script.UpdateControls();

        model2 = avatars.getPlayer2Avatar(out prefab2, out colors2, out size2);

        leftLimit2 = -7050.0f + ((size1 - 300) * -0.467f);
        rightLimit2 = -8540.0f + ((size1 - 300) * 0.533f);

        /*if (model2.Equals("Flower"))
        {
            leftLimit2 = -7050.0f + ((size1 - 300) * -0.267f);
            rightLimit2 = -8560.0f + ((size1 - 300) * 0.583f);
        }
        else if (model2.Equals("Sword"))
        {
            leftLimit2 = -7050.0f + ((size1 - 300) * -0.267f);
            rightLimit2 = -8560.0f + ((size1 - 300) * 0.583f);
        }
        else if (model2.Equals("Normal"))
        {
            leftLimit2 = -7050.0f + ((size1 - 300) * -0.267f);
            rightLimit2 = -8560.0f + ((size1 - 300) * 0.583f);
        }*/
        GameObject stick2 = Instantiate(prefab2, new Vector3(), new Quaternion());
        stick2.GetComponent<Rigidbody>().position = new Vector3(-1500.0f, 0.0f, -7745.6f);
        stick2.tag = "Stick2";

        stickSize = stick2.GetComponent<Transform>().localScale;
        stickSize.z = size2;
        stick2.GetComponent<Transform>().localScale = stickSize;

        stick2.AddComponent<AudioSource>();
        stick2.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("palletHit");
        stick2.GetComponent<AudioSource>().playOnAwake = false;
        if (model2.Equals("Flower"))
        {
            var children = stick2.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.name.Equals("Stem"))
                {
                    if (!colors2[0].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors2[0];
                    }
                }
                else if (child.name.Equals("Middle"))
                {
                    if (!colors2[1].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors2[1];
                    }
                }
                else if (child.name.Contains("Petal"))
                {
                    if (!colors2[2].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors2[2];
                    }
                }
            }
        }
        if (model2.Equals("Sword"))
        {
            var children = stick2.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.name == "Blade")
                {
                    if (!colors2[0].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors2[0];
                    }
                }
                else if (child.name == "Crossguard")
                {
                    if (!colors2[1].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors2[1];
                    }
                }
                else if (child.name == "Grip")
                {
                    if (!colors2[2].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors2[2];
                    }
                }
                else if (child.name == "Pomel")
                {
                    if (!colors2[3].Equals(Color.clear))
                    {
                        child.GetComponent<Renderer>().material.color = colors2[3];
                    }
                }
            }
        }
        if (model2.Equals("Normal"))
        {
            if (!colors2[0].Equals(Color.clear))
            {
                stick2.GetComponent<Renderer>().material.color = colors2[0];
            }
        }

        Stick stick2Script = stick2.GetComponent<Stick>();
        stick2Script.setStickOptions(leftLimit2, rightLimit2, false);
        stick2Script.UpdateControls();
    }
}
