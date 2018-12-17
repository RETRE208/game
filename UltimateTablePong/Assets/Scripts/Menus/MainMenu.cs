﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private GameObject mainPanel;
    private GameObject mainText;
    private GameObject mainStartButton;
    private GameObject mainQuitButton;
    private GameObject mainKeybindButton;
    private GameObject mainAvatarsButton;
    private GameObject mainInitialKeyText;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    private SettingsMenu settingsMenu;
    private Keybind keybindsMenu;
    private Avatars avatars;
    
    private bool onTitleScreen = true;

    // Use this for initialization
    void Start () {
        settingsMenu = GameObject.FindObjectOfType<SettingsMenu>();
        GameObject keybindController = GameObject.FindGameObjectWithTag("KeybindController");
        if (keybindController != null)
        {
            keybindsMenu = keybindController.GetComponent<Keybind>();
            if (keybindsMenu == null)
            {
                Debug.Log("Cannot find 'Keybind' script");
            }
        }
        else
        {
            Debug.Log("Cannot find 'KeybindController' object");
        }

        avatars = GameObject.FindObjectOfType<Avatars>();

        Time.timeScale = 0;
        
        mainPanel = GameObject.Find("MainPanel");
        mainText = GameObject.Find("MainText");
        mainStartButton = GameObject.Find("MainStartButton");
        mainQuitButton = GameObject.Find("MainQuitButton");
        mainKeybindButton = GameObject.Find("MainKeybindButton");
        mainInitialKeyText = GameObject.Find("MainInitialKeyText");
        mainAvatarsButton = GameObject.Find("MainAvatarsButton");

        scoreText = GameObject.Find("ScoreText");
        playerReady = GameObject.Find("PlayerReady");
        gameInfo = GameObject.Find("GameInfo");

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);

        mainStartButton.GetComponent<Button>().onClick.AddListener(DisplaySettingsMenu);
        mainQuitButton.GetComponent<Button>().onClick.AddListener(Application.Quit);
        mainKeybindButton.GetComponent<Button>().onClick.AddListener(DisplayKeybindMenu);
        mainAvatarsButton.GetComponent<Button>().onClick.AddListener(DisplayAvatarsMenu);

        mainStartButton.transform.localScale = new Vector3(0, 0, 0);
        mainQuitButton.transform.localScale = new Vector3(0, 0, 0);
        mainKeybindButton.transform.localScale = new Vector3(0, 0, 0);
        mainAvatarsButton.transform.localScale = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey && onTitleScreen)
        {
            onTitleScreen = false;
            mainInitialKeyText.SetActive(false);
            mainStartButton.transform.localScale = new Vector3(1, 1, 1);
            mainQuitButton.transform.localScale = new Vector3(1, 1, 1);
            mainKeybindButton.transform.localScale = new Vector3(1, 1, 1);
            mainAvatarsButton.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void DisplayMainMenu()
    {
        Time.timeScale = 0;
        mainPanel.SetActive(true);
        mainText.SetActive(true);
        mainStartButton.SetActive(true);
        mainQuitButton.SetActive(true);
        mainKeybindButton.SetActive(true);
        mainAvatarsButton.SetActive(true);

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);
    }

    public void HideMainMenu()
    {
        Time.timeScale = 1;
        mainPanel.SetActive(false);
        mainText.SetActive(false);
        mainStartButton.SetActive(false);
        mainQuitButton.SetActive(false);
        mainKeybindButton.SetActive(false);
        mainAvatarsButton.SetActive(false);

        scoreText.transform.localScale = new Vector3(1, 1, 1);
        playerReady.transform.localScale = new Vector3(2, 2, 2);
        gameInfo.transform.localScale = new Vector3(1, 1, 1);
    }

   void DisplaySettingsMenu()
    {
        HideMainMenu();
        settingsMenu.DisplaySettingsMenu();
    }

    void DisplayKeybindMenu()
    {
        HideMainMenu();
        keybindsMenu.DisplayKeybindMenu();
    }

    void DisplayAvatarsMenu()
    {
        HideMainMenu();
        avatars.DisplayMenu();
    }
}
