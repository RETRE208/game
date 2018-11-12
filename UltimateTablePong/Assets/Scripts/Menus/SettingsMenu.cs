using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    private GameObject settingsPanel;
    private GameObject settingsText;
    private GameObject settingsStartButton;
    private GameObject settingsBackButton;

    private GameObject turnsSlider;
    private GameObject turnsText;
    private GameObject turnNumberText;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    private MainMenu mainMenu;
    private GameController gameController;

    private int numberOfRounds;

    // Use this for initialization
    void Start()
    {
        mainMenu = gameObject.GetComponent<MainMenu>();
        gameController = GameObject.FindObjectOfType<GameController>();

        Time.timeScale = 0;
        settingsPanel = GameObject.Find("SettingsPanel");
        settingsText = GameObject.Find("SettingsText");
        settingsStartButton = GameObject.Find("SettingsStartButton");
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

        settingsStartButton.GetComponent<Button>().onClick.AddListener(StartGame);
        settingsBackButton.GetComponent<Button>().onClick.AddListener(DisplayMainMenu);
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
        settingsBackButton.SetActive(false);
        turnsSlider.SetActive(false);
        turnsText.SetActive(false);
        turnNumberText.SetActive(false);

        scoreText.SetActive(true);
        playerReady.SetActive(true);
        gameInfo.SetActive(true);
        //scoreText.transform.localScale = new Vector3(1, 1, 1);
        //playerReady.transform.localScale = new Vector3(2, 2, 2);
        //gameInfo.transform.localScale = new Vector3(1, 1, 1);
    }

    void DisplayMainMenu()
    {
        turnsSlider.GetComponent<Slider>().value = turnsSlider.GetComponent<Slider>().minValue;
        HideSettingsMenu();
        mainMenu.DisplayMainMenu();
    }

    void StartGame()
    {
        gameController.numberOfRounds = (int)(turnsSlider.GetComponent<Slider>().value);
        turnsSlider.GetComponent<Slider>().value = turnsSlider.GetComponent<Slider>().minValue;
        HideSettingsMenu();
    }
}
