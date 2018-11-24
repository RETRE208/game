﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    private GameObject settingsPanel;
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
    private GameObject gameInfo;

    private MainMenu mainMenu;
    private GameController gameController;
    private PauseMenu pauseMenu;
    private AI ai;

    private GameObject stick;
    private Stick stickScript;
    private GameObject stick2;
    private Stick stickScript2;

    private int numberOfRounds;

    /*
    settingsCreateOnlineButton
    settingsJoinOnlineButton
    settingsStartAiEasyButton
    settingsStartAiHardButton
    */

    // Use this for initialization
    void Start()
    {
        mainMenu = gameObject.GetComponent<MainMenu>();
        gameController = GameObject.FindObjectOfType<GameController>();
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        ai = GameObject.FindObjectOfType<AI>();

        Time.timeScale = 0;
        settingsPanel = GameObject.Find("SettingsPanel");
        settingsText = GameObject.Find("SettingsText");
        settingsStartButton = GameObject.Find("SettingsStartButton");
        settingsCreateOnlineButton = GameObject.Find("SettingsCreateOnlineButton");
        settingsJoinOnlineButton = GameObject.Find("SettingsJoinOnlineButton");
        settingsStartAiEasyButton = GameObject.Find("SettingsStartAiEasyButton");
        settingsStartAiHardButton = GameObject.Find("SettingsStartAiHardButton");
        settingsBackButton = GameObject.Find("SettingsBackButton");
        turnsSlider = GameObject.Find("TurnsSlider"); ;
        turnsText = GameObject.Find("TurnsText"); ;
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
        settingsJoinOnlineButton.GetComponent<Button>().onClick.AddListener(JoinOnlineGame);
        settingsStartAiEasyButton.GetComponent<Button>().onClick.AddListener(StartVsAiEasy);
        settingsStartAiHardButton.GetComponent<Button>().onClick.AddListener(StartVsAiHard);
        settingsBackButton.GetComponent<Button>().onClick.AddListener(DisplayMainMenu);

        GameObject stick = GameObject.FindGameObjectWithTag("Stick");
        if (stick != null)
        {
            stickScript = stick.GetComponent<Stick>();
        }
        GameObject stick2 = GameObject.FindGameObjectWithTag("Stick2");
        if (stick2 != null)
        {
            stickScript2 = stick2.GetComponent<Stick>();
        }
    }

    // Update is called once per frame
    void Update()
    {
            turnNumberText.GetComponent<Text>().text = turnsSlider.GetComponent<Slider>().value.ToString();
    }

    public void DisplaySettingsMenu()
    {
        Time.timeScale = 0;
        settingsPanel.SetActive(true);
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
        //scoreText.transform.localScale = new Vector3(0, 0, 0);
        //playerReady.transform.localScale = new Vector3(0, 0, 0);
        //gameInfo.transform.localScale = new Vector3(0, 0, 0);
    }

    public void HideSettingsMenu()
    {
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
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
        gameInfo.SetActive(true);

        stickScript.UpdateControls();
        stickScript2.UpdateControls();
        pauseMenu.UpdatePauseButton();
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
        ai.removeAi();
        HideSettingsMenu();
    }

    void CreateOnlineGame()
    {
        setGameSettings();
        ai.removeAi();
        //Create online game
        HideSettingsMenu();
    }

    void JoinOnlineGame()
    {
        ai.removeAi();
        //Join online game
        HideSettingsMenu();
    }

    void StartVsAiEasy()
    {
        setGameSettings();
        ai.setSimulationEasy();
        HideSettingsMenu();
    }

    void StartVsAiHard()
    {
        setGameSettings();
        ai.setSimulationHard();
        HideSettingsMenu();
    }

    private void setGameSettings()
    {
        gameController.numberOfRounds = (int)(turnsSlider.GetComponent<Slider>().value);
        turnsSlider.GetComponent<Slider>().value = turnsSlider.GetComponent<Slider>().minValue;
    }
}
