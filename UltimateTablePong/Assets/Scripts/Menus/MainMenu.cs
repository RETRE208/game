using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private GameObject mainPanel;
    private GameObject mainBackground;
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
    private GameObject seedText;
    private GameObject OkSeed;
    private GameObject InputFieldSeed;
    private GameObject InputSeedIDText;

    private GameObject musicChoiceText;
    private GameObject musicDropdown;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    private SettingsMenu settingsMenu;
    private Keybind keybindsMenu;
    private Avatars avatars;
    private GameController gameController;
    public InputField inputSeed;

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
        }

        avatars = GameObject.FindObjectOfType<Avatars>();
        gameController = GameObject.FindObjectOfType<GameController>();

        Time.timeScale = 0;
        
        mainPanel = GameObject.Find("MainPanel");
        mainBackground = GameObject.Find("MainBackground");
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
        musicChoiceText = GameObject.Find("MusicChoiceText");
        musicDropdown = GameObject.Find("MusicDropdown");
        seedText = GameObject.Find("SeedText");
        OkSeed = GameObject.Find("OkSeed");
        InputFieldSeed = GameObject.Find("InputFieldSeed");
        InputSeedIDText = GameObject.Find("InputSeedIDText");

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
        musicChoiceText.transform.localScale = new Vector3(0, 0, 0);
        musicDropdown.transform.localScale = new Vector3(0, 0, 0);
        seedText.transform.localScale = new Vector3(0, 0, 0);
        OkSeed.transform.localScale = new Vector3(0, 0, 0);
        InputFieldSeed.transform.localScale = new Vector3(0, 0, 0);
        InputSeedIDText.transform.localScale = new Vector3(0, 0, 0);

        soundManager.playMusic("mainMenu");

        Time.timeScale = 1;
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
            musicChoiceText.transform.localScale = new Vector3(1, 1, 1);
            musicDropdown.transform.localScale = new Vector3(1, 1, 1);
            seedText.transform.localScale = new Vector3(1, 1, 1);
            OkSeed.transform.localScale = new Vector3(1, 1, 1);
            InputFieldSeed.transform.localScale = new Vector3(1, 1, 1);
            InputSeedIDText.transform.localScale = new Vector3(1, 1, 1);
        }
        ChangeSfxVolume();
        ChangeMusicVolume();
        ChangeAmbiantVolume();

        sfxVolumeValue.GetComponent<Text>().text = ((int)(sfxSlider.GetComponent<Slider>().value * 100)).ToString();
        musicVolumeValue.GetComponent<Text>().text = ((int)(musicSlider.GetComponent<Slider>().value * 200)).ToString();
        ambiantVolumeValue.GetComponent<Text>().text = ((int)(ambiantSlider.GetComponent<Slider>().value * 100)).ToString();
        soundManager.chooseInGameMusic(musicDropdown.GetComponent<Dropdown>().options[musicDropdown.GetComponent<Dropdown>().value].text);
    }

    public void DisplayMainMenu()
    {
        Time.timeScale = 0;
        mainPanel.SetActive(true);
        mainBackground.SetActive(true);
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
        musicChoiceText.SetActive(true);
        musicDropdown.SetActive(true);
        seedText.SetActive(true);
        OkSeed.SetActive(true);
        InputFieldSeed.SetActive(true);
        InputSeedIDText.SetActive(true);

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);
        soundManager.playMusic("mainMenu");

        
        Time.timeScale = 1;
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
        mainBackground.SetActive(false);
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
        musicChoiceText.SetActive(false);
        musicDropdown.SetActive(false);
        seedText.SetActive(false);
        OkSeed.SetActive(false);
        InputFieldSeed.SetActive(false);
        InputSeedIDText.SetActive(false);

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

    public void SpawnObstaclesWithSeed()
    {
        string id = inputSeed.text.ToString();
        string coord = PlayerPrefs.GetString(id, "");
        if (coord != "")
        {
            gameController.SpawnObstaclesWithSeed(id, coord);
        }
        
    }
}
