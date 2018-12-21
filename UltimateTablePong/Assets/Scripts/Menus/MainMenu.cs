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
    private GameObject sfxSlider;
    private GameObject musicSlider;
    private GameObject ambiantSlider;

    private GameObject sfxVolumeText;
    private GameObject sfxVolumeValue;
    private GameObject musicVolumeText;
    private GameObject musicVolumeValue;
    private GameObject ambiantVolumeText;
    private GameObject ambiantVolumeValue;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    private SettingsMenu settingsMenu;
    private Keybind keybindsMenu;
    private Avatars avatars;

    private SoundManager soundManager;
    
    private bool onTitleScreen = true;

    // Use this for initialization
    void Start () {
        soundManager = FindObjectOfType<SoundManager>();
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
        sfxSlider = GameObject.Find("SFXSlider");
        musicSlider = GameObject.Find("MusicSlider");
        ambiantSlider = GameObject.Find("AmbiantSlider");
        sfxVolumeText = GameObject.Find("SFXVolumeText");
        sfxVolumeValue = GameObject.Find("SFXVolumeValue");
        musicVolumeText = GameObject.Find("MusicVolumeText");
        musicVolumeValue = GameObject.Find("MusicVolumeValue");
        ambiantVolumeText = GameObject.Find("AmbiantVolumeText");
        ambiantVolumeValue = GameObject.Find("AmbiantVolumeValue");

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
        sfxSlider.transform.localScale = new Vector3(0, 0, 0);
        musicSlider.transform.localScale = new Vector3(0, 0, 0);
        ambiantSlider.transform.localScale = new Vector3(0, 0, 0);
        sfxVolumeText.transform.localScale = new Vector3(0, 0, 0);
        sfxVolumeValue.transform.localScale = new Vector3(0, 0, 0);
        musicVolumeText.transform.localScale = new Vector3(0, 0, 0);
        musicVolumeValue.transform.localScale = new Vector3(0, 0, 0);
        ambiantVolumeText.transform.localScale = new Vector3(0, 0, 0);
        ambiantVolumeValue.transform.localScale = new Vector3(0, 0, 0);

        soundManager.playMusic("mainMenu");
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
            sfxSlider.transform.localScale = new Vector3(1, 1, 1);
            musicSlider.transform.localScale = new Vector3(1, 1, 1);
            ambiantSlider.transform.localScale = new Vector3(1, 1, 1);
            sfxVolumeText.transform.localScale = new Vector3(1, 1, 1);
            sfxVolumeValue.transform.localScale = new Vector3(1, 1, 1);
            musicVolumeText.transform.localScale = new Vector3(1, 1, 1);
            musicVolumeValue.transform.localScale = new Vector3(1, 1, 1);
            ambiantVolumeText.transform.localScale = new Vector3(1, 1, 1);
            ambiantVolumeValue.transform.localScale = new Vector3(1, 1, 1);
        }
        ChangeSfxVolume();
        ChangeMusicVolume();
        ChangeAmbiantVolume();

        sfxVolumeValue.GetComponent<Text>().text = ((int)(sfxSlider.GetComponent<Slider>().value * 100)).ToString();
        musicVolumeValue.GetComponent<Text>().text = ((int)(musicSlider.GetComponent<Slider>().value * 200)).ToString();
        ambiantVolumeValue.GetComponent<Text>().text = ((int)(ambiantSlider.GetComponent<Slider>().value * 100)).ToString();
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
        sfxSlider.SetActive(true);
        musicSlider.SetActive(true);
        ambiantSlider.SetActive(true);
        sfxVolumeText.SetActive(true);
        sfxVolumeValue.SetActive(true);
        musicVolumeText.SetActive(true);
        musicVolumeValue.SetActive(true);
        ambiantVolumeText.SetActive(true);
        ambiantVolumeValue.SetActive(true);

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);
        soundManager.playMusic("mainMenu");
    }

    public void ChangeSfxVolume()
    {
        soundManager.changeSfxVolume(sfxSlider.GetComponent<Slider>().value);
    }

    public void ChangeMusicVolume()
    {
        soundManager.changeMusicVolume(musicSlider.GetComponent<Slider>().value);
    }

    public void ChangeAmbiantVolume()
    {
        soundManager.changeAmbiantVolume(ambiantSlider.GetComponent<Slider>().value * 2);
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
        sfxSlider.SetActive(false);
        musicSlider.SetActive(false);
        ambiantSlider.SetActive(false);
        sfxVolumeText.SetActive(false);
        sfxVolumeValue.SetActive(false);
        musicVolumeText.SetActive(false);
        musicVolumeValue.SetActive(false);
        ambiantVolumeText.SetActive(false);
        ambiantVolumeValue.SetActive(false);

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
