using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private GameObject mainPanel;
    private GameObject mainText;
    private GameObject mainStartButton;
    private GameObject mainQuitButton;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    private SettingsMenu settingsMenu;

    // Use this for initialization
    void Start () {
        settingsMenu = gameObject.GetComponent<SettingsMenu>();

        Time.timeScale = 0;
        
        mainPanel = GameObject.Find("MainPanel");
        mainText = GameObject.Find("MainText");
        mainStartButton = GameObject.Find("MainStartButton");
        mainQuitButton = GameObject.Find("MainQuitButton");

        scoreText = GameObject.Find("ScoreText");
        playerReady = GameObject.Find("PlayerReady");
        gameInfo = GameObject.Find("GameInfo");

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);

        mainStartButton.GetComponent<Button>().onClick.AddListener(DisplaySettingsMenu);
        mainQuitButton.GetComponent<Button>().onClick.AddListener(Application.Quit);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void DisplayMainMenu()
    {
        Time.timeScale = 0;
        mainPanel.SetActive(true);
        mainText.SetActive(true);
        mainStartButton.SetActive(true);
        mainQuitButton.SetActive(true);
        
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

        scoreText.transform.localScale = new Vector3(1, 1, 1);
        playerReady.transform.localScale = new Vector3(2, 2, 2);
        gameInfo.transform.localScale = new Vector3(1, 1, 1);
    }

   void DisplaySettingsMenu()
    {
        HideMainMenu();
        settingsMenu.DisplaySettingsMenu();
    }
}
