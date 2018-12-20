using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndMenu : MonoBehaviour {

    private GameObject endPanel;
    private GameObject endText;
    private GameObject endContextText;
    private GameObject quickRestartButton;
    private GameObject backToMainMenuButton;
    private GameObject quitButton;

    private MainMenu mainMenu;
    private UnityAction restartGame;
    private UnityAction leaveGame;
    private SoundManager soundManager;

    // Use this for initialization
    void Start () {
        soundManager = FindObjectOfType<SoundManager>();
        mainMenu = gameObject.GetComponent<MainMenu>();

        endPanel = GameObject.Find("EndPanel");
        endText = GameObject.Find("EndText");
        endContextText = GameObject.Find("EndContextText");
        quickRestartButton = GameObject.Find("QuickRestartButton");
        backToMainMenuButton = GameObject.Find("BackToMainMenuButton");
        quitButton = GameObject.Find("QuitButton");

        endPanel.SetActive(false);
        endText.SetActive(false);
        endContextText.SetActive(false);
        quickRestartButton.SetActive(false);
        backToMainMenuButton.SetActive(false);
        quitButton.SetActive(false);

        backToMainMenuButton.GetComponent<Button>().onClick.AddListener(DisplayMainMenu);
        quitButton.GetComponent<Button>().onClick.AddListener(Application.Quit);
    }

    void Update()
    {
        
    }

    public void SetRestartGameAction(UnityAction restartGame)
    {
        this.restartGame = restartGame;
    }

    public void SetLeaveGameAction(UnityAction leaveGame)
    {
        this.leaveGame = leaveGame;
    }

    public void DisplayEndMenu(string displayText)
    {
        Time.timeScale = 0;
        endContextText.GetComponent<Text>().text = displayText;
        endPanel.SetActive(true);
        endText.SetActive(true);
        endContextText.SetActive(true);
        quickRestartButton.SetActive(true);
        backToMainMenuButton.SetActive(true);
        quitButton.SetActive(true);

        quickRestartButton.GetComponent<Button>().onClick.AddListener(restartGame);

        soundManager.playMusic("gameEnd");
    }

    public void HideEndMenu()
    {
        Time.timeScale = 1;
        endPanel.SetActive(false);
        endText.SetActive(false);
        endContextText.SetActive(false);
        quickRestartButton.SetActive(false);
        backToMainMenuButton.SetActive(false);
        quitButton.SetActive(false);
    }

    void DisplayMainMenu()
    {
        HideEndMenu();
        leaveGame();
        mainMenu.DisplayMainMenu();
    }
}
