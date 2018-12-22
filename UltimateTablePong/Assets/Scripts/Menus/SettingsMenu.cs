using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SettingsMenu : MonoBehaviour {

    private GameObject settingsPanel;
    private GameObject settingsBackground;
    private GameObject settingsText;
    private GameObject settingsStartButton;
    private GameObject settingsCreateOnlineButton;
    private GameObject settingsJoinOnlineButton;
    private GameObject settingsStartAiEasyButton;
    private GameObject settingsStartAiHardButton;
    private GameObject settingsBackButton;

    private GameObject turnsSlider;
    private GameObject turnsText;
    private GameObject turnNumberText;

    private GameObject scoreText;
    private GameObject playerReady;
    public Text playerReadyText;
    private GameObject gameInfo;

    private MainMenu mainMenu;
    private GameController gameController;
    private PauseMenu pauseMenu;
    private AI ai;
    private LoadingScreen loadingScreen;
    private Online OnlineMenu;
    private GameObject stick;
    private Stick stickScript;
    private GameObject stick2;
    private Stick stickScript2;

    private int numberOfRounds;

    private KeyCode startButton;
    private Keybind keybindsMenu;
    public GameObject stick1OnlinePrefab;
    public GameObject stick2OnlinePrefab;

    // Use this for initialization
    void Start()
    {
        mainMenu = gameObject.GetComponent<MainMenu>();
        gameController = GameObject.FindObjectOfType<GameController>();
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        loadingScreen = GameObject.FindObjectOfType<LoadingScreen>();
        ai = GameObject.FindObjectOfType<AI>();

        settingsPanel = GameObject.Find("SettingsPanel");
        settingsBackground = GameObject.Find("SettingsBackground");
        settingsText = GameObject.Find("SettingsText");
        settingsStartButton = GameObject.Find("SettingsStartButton");
        settingsCreateOnlineButton = GameObject.Find("SettingsCreateOnlineButton");
        settingsJoinOnlineButton = GameObject.Find("SettingsJoinOnlineButton");
        settingsStartAiEasyButton = GameObject.Find("SettingsStartAiEasyButton");
        settingsStartAiHardButton = GameObject.Find("SettingsStartAiHardButton");
        settingsBackButton = GameObject.Find("SettingsBackButton");
        turnsSlider = GameObject.Find("TurnsSlider");
        turnsText = GameObject.Find("TurnsText");
        turnNumberText = GameObject.Find("TurnsNumberText");

        scoreText = GameObject.Find("ScoreText");
        playerReady = GameObject.Find("PlayerReady");
        gameInfo = GameObject.Find("GameInfo");

        settingsPanel.SetActive(false);
        settingsText.SetActive(false);
        settingsStartButton.SetActive(false);

        settingsBackButton.SetActive(false);
        turnsSlider.SetActive(false);
        turnsText.SetActive(false);
        turnNumberText.SetActive(false);

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);

        settingsStartButton.GetComponent<Button>().onClick.AddListener(StartGameLocalPvP);
        settingsCreateOnlineButton.GetComponent<Button>().onClick.AddListener(CreateOnlineGame);
        settingsJoinOnlineButton.GetComponent<Button>().onClick.AddListener(DisplayOnlineMenu);
        settingsStartAiEasyButton.GetComponent<Button>().onClick.AddListener(StartVsAiEasy);
        settingsStartAiHardButton.GetComponent<Button>().onClick.AddListener(StartVsAiHard);
        settingsBackButton.GetComponent<Button>().onClick.AddListener(DisplayMainMenu);

        stick = GameObject.FindGameObjectWithTag("Stick");
        if (stick != null)
        {
            stickScript = stick.GetComponent<Stick>();
        }
        stick2 = GameObject.FindGameObjectWithTag("Stick2");
        if (stick2 != null)
        {
            stickScript2 = stick2.GetComponent<Stick>();
        }

        GameObject OnlineController = GameObject.FindGameObjectWithTag("OnlineController");
        if (OnlineController != null)
        {
            OnlineMenu = OnlineController.GetComponent<Online>();
        }

        GameObject keybindController = GameObject.FindGameObjectWithTag("KeybindController");
        if (keybindController != null)
        {
            keybindsMenu = keybindController.GetComponent<Keybind>();
        }

        playerReadyText = playerReady.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        turnNumberText.GetComponent<Text>().text = turnsSlider.GetComponent<Slider>().value.ToString();
    }

    public void DisplaySettingsMenu()
    {
        Time.timeScale = 1;
        settingsPanel.SetActive(true);
        settingsBackground.SetActive(true);
        settingsText.SetActive(true);
        settingsStartButton.SetActive(true);
        settingsCreateOnlineButton.SetActive(true);
        settingsJoinOnlineButton.SetActive(true);
        settingsStartAiEasyButton.SetActive(true);
        settingsStartAiHardButton.SetActive(true);
        settingsBackButton.SetActive(true);
        turnsSlider.SetActive(true);
        turnsText.SetActive(true);
        turnNumberText.SetActive(true);

        scoreText.SetActive(false);
        playerReady.SetActive(false);
        gameInfo.SetActive(false);
    }

    public void HideSettingsMenu()
    {
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
        settingsBackground.SetActive(false);
        settingsText.SetActive(false);
        settingsStartButton.SetActive(false);
        settingsCreateOnlineButton.SetActive(false);
        settingsJoinOnlineButton.SetActive(false);
        settingsStartAiEasyButton.SetActive(false);
        settingsStartAiHardButton.SetActive(false);
        settingsBackButton.SetActive(false);
        turnsSlider.SetActive(false);
        turnsText.SetActive(false);
        turnNumberText.SetActive(false);

        scoreText.SetActive(true);
        playerReady.SetActive(true);
        startButton = keybindsMenu.GetStartButton();
        playerReadyText.text = "Player one's turn, \n Press the start button(" + startButton.ToString() + ") to start!";
        gameInfo.SetActive(true);

        pauseMenu.UpdatePauseButton();

        gameController.UpdateStartButton();

        stick = GameObject.FindGameObjectWithTag("Stick");
        if (stick != null)
        {
            stickScript = stick.GetComponent<Stick>();
        }
        stick2 = GameObject.FindGameObjectWithTag("Stick2");
        if (stick2 != null)
        {
            stickScript2 = stick2.GetComponent<Stick>();
        }
        if (stickScript != null)
        {
            stickScript.UpdateControls();
        }
        if (stickScript2 != null)
        {
            stickScript2.UpdateControls();
        }

        loadingScreen.displayLoadingScreen();
    }

    void DisplayMainMenu()
    {
        turnsSlider.GetComponent<Slider>().value = turnsSlider.GetComponent<Slider>().minValue;
        HideSettingsMenu();
        mainMenu.DisplayMainMenu();
    }

    void StartGameLocalPvP()
    {
        setGameSettings();
        gameController.createPlayersSticks();
        HideSettingsMenu();
    }

    void CreateOnlineGame()
    {
        //Create online game
        HideSettingsMenu();

        gameController.DestroyAllSticks();

        NetworkManagerCustom manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerCustom>();
        manager.StartHost();

        gameController.setOnlineMode(true, true);
        StartCoroutine(SetupPlayer1());
    }

    public void JoinOnlineGame(string ip)
    {
        //Join online game
        HideSettingsMenu();

        gameController.DestroyAllSticks();

        NetworkManagerCustom manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerCustom>();
        manager.networkAddress = ip;
        manager.StartClient();

        gameController.setOnlineMode(true, false);
        StartCoroutine(SetupPlayer2());
    }

    IEnumerator SetupPlayer1()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject[] sticksOnline = GameObject.FindGameObjectsWithTag("StickOnline");
        foreach (GameObject stick in sticksOnline)
        {
            if (stick.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                stick.GetComponent<Rigidbody>().position = new Vector3(-1500.0f, 0.0f, 42.0f);
                stick.tag = "Stick";

                Stick stickOnlineScript = stick.GetComponent<Stick>();
                stickOnlineScript.setStickOptionsForOnline(775.0f, -775.0f, true, false);
                stickOnlineScript.UpdateControls();
            }
            else
            {
                Stick stickOnlineScript = stick.GetComponent<Stick>();
                stickOnlineScript.setStickOptionsForOnline(-7050.0f, -8600.0f, false, false);
            }
        }
    }

    IEnumerator SetupPlayer2()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject[] sticksOnline = GameObject.FindGameObjectsWithTag("StickOnline");
        foreach (GameObject stick in sticksOnline)
        {
            if (stick.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                stick.GetComponent<Rigidbody>().position = new Vector3(-1500.0f, 0.0f, -7745.6f);
                stick.tag = "Stick2";

                Stick stickOnlineScript = stick.GetComponent<Stick>();
                stickOnlineScript.setStickOptionsForOnline(-7050.0f, -8600.0f, false, false);
                stickOnlineScript.UpdateControls();
            }
        }
    }

    void StartVsAiEasy()
    {
        setGameSettings();
        gameController.createPlayersSticks();
        gameController.aiMode = true;
        ai.activateAI();
        ai.UnpauseAI();
        ai.setSimulationEasy();
        HideSettingsMenu();
    }

    void StartVsAiHard()
    {
        setGameSettings();
        gameController.createPlayersSticks();
        gameController.aiMode = true;
        ai.activateAI();
        ai.UnpauseAI();
        ai.setSimulationHard();
        HideSettingsMenu();
    }

    private void setGameSettings()
    {
        gameController.numberOfRounds = (int)(turnsSlider.GetComponent<Slider>().value);
        turnsSlider.GetComponent<Slider>().value = turnsSlider.GetComponent<Slider>().minValue;
    }

    void DisplayOnlineMenu()
    {
        HideSettingsMenu();
        OnlineMenu.DisplayOnlineMenu();
    }
}
