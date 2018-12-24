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
    public GameObject obstacle;
    public Vector3 spawnValues;
    private bool player1turn;
    public int numberOfRounds;
    private int currentRound;
    private int numberOfHit;
    private KeyCode startButton;
    private string seed;
    private int scoreColor;

    private EndMenu endMenu;
    private PauseMenu pauseMenu;
    private Keybind keybindsMenu;
    private Avatars avatars;

    private bool isOnlineMode;
    private bool isHost;
    private bool isPlayer2Connected;
    private BallSpawner ballSpawner;

    private SoundManager soundManager;
    private GameObject oneUpSound;

    public Text seedText;
    public bool aiMode;

    public Material material;

    public GameObject stickPrefab;

    // Use this for initialization
    void Start () {
        oneUpSound = GameObject.Find("OneUpSound");
        soundManager = FindObjectOfType<SoundManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        endMenu = FindObjectOfType<EndMenu>();

        currentRound = 1;
        player1turn = true;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        scoreColor = 0;
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
        SpawnObstacles();
        material.color = Color.red;
    }

    public void UpdateStartButton()
    {
        startButton = keybindsMenu.GetStartButton();
    }

    void FixedUpdate()
    {
        if (player1turn)
        {
            scoreColor = scorePlayer1;
            if(scoreColor < 35)
            {
                material.color = Color.red;
            }
            if (scoreColor > 35 && scoreColor < 100)
            {
                material.color = Color.blue;
            }
            if (scoreColor > 100 && scoreColor < 200)
            {
                material.color = Color.green;
            }
            if (scoreColor > 200)
            {
                material.color = Color.black;
            }
        }
        else
        {
            scoreColor = scorePlayer2;
            if (scoreColor < 35)
            {
                material.color = Color.red;
            }
            if (scoreColor > 35 && scoreColor < 100)
            {
                material.color = Color.blue;
            }
            if (scoreColor > 100 && scoreColor < 200)
            {
                material.color = Color.green;
            }
            if (scoreColor > 200)
            {
                material.color = Color.black;
            }
        }

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
        oneUpSound.GetComponent<AudioSource>().volume = soundManager.sfxVolume;
        if (!isOnlineMode)
        {
            numberOfHit += 1;
            if (ballCount == 1 && numberOfHit == 3)
            {
                soundManager.changeMusicPitch(1.1f);
                oneUpSound.GetComponent<AudioSource>().Play();
                SpawnBall();
                numberOfHit = 0;
            }
            if (ballCount == 2 && numberOfHit == 5)
            {
                soundManager.changeMusicPitch(1.2f);
                oneUpSound.GetComponent<AudioSource>().Play();
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
                oneUpSound.GetComponent<AudioSource>().Play();
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
        scoreColor = 0;
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

    public void SpawnObstacles()
    {
        Vector3 spawnPosition;
        float playerOneZ = -530.0f;
        float playerTwoZ = -8250.0f;
        float x = -132.0f;
        float z = 0.0f;
        spawnPosition = new Vector3(x, 100.0f, z);
        float randomX = 0.0f;
        float randomZ = 0.0f;
        float positionX = 0.0f;
        float positionZ = 0.0f;
        float positionZ2 = 0.0f;
        string coord = "";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                randomX = UnityEngine.Random.Range(-430.0f, 430.0f);
                randomZ = UnityEngine.Random.Range(-250.0f, 250.0f);
                positionX = x + randomX;
                positionZ = playerOneZ + randomZ;
                positionZ2 = playerTwoZ + randomZ;
                spawnPosition = new Vector3(positionX, 100.0f, positionZ);
                GameObject obst = obstacle;
                GameObject obst2 = obstacle;
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(obst, spawnPosition, spawnRotation);
                spawnPosition = new Vector3(positionX, 100.0f, positionZ2);
                Instantiate(obst2, spawnPosition, spawnRotation);
                playerOneZ += 543;
                playerTwoZ += 543;
                coord += positionX.ToString() + ";" + positionZ + ";" + positionZ2 + "/";
            }
            x += 936;
            playerOneZ = -530.0f;
            playerTwoZ = -8250.0f;
        }
        seed = GenerateSeed();
        SaveSeed(seed, coord);
        seedText.text = "Current map seed : " + seed;
    }

    private void DestroyAllObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    public void ChangeMap()
    {
        DestroyAllObstacles();
    }

    private string GenerateSeed()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmssf");
    }

    public void SaveSeed(string id, string coord)
    {
       PlayerPrefs.SetString(id, coord);
    }

    public void SpawnObstaclesWithSeed(string id, string coord)
    {
        if (coord != "")
        {
            Debug.Log(coord);
            DestroyAllObstacles();
            string[] coords = coord.Split('/');
            for (int i = 0; i < 9; i++)
            {
                Debug.Log("c = " + coords[i]);
                string[] values = coords[i].Split(';');
                Vector3 spawnPosition = new Vector3(float.Parse(values[0]), 100.0f, float.Parse(values[1]));
                GameObject obst = obstacle;
                GameObject obst2 = obstacle;
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(obst, spawnPosition, spawnRotation);
                spawnPosition = new Vector3(float.Parse(values[0]), 100.0f, float.Parse(values[2]));
                Instantiate(obst2, spawnPosition, spawnRotation);
            }
            seedText.text = "Current map seed : " + id;
        }
    }

    public void RandomSeed()
    {
        DestroyAllObstacles();
        SpawnObstacles();
    }

    public void ChangeColor()
    {
        
    }
}
