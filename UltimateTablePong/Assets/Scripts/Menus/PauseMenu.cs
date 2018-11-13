using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour {

    private bool playerReadyState;
    private bool paused;

    private GameObject pauseTitle;
    private GameObject pausePanel; 
    private GameObject playerReady;
    private GameObject pauseMainMenuButton;
    private GameObject pauseResumeButton;

    private MainMenu mainMenu;
    private UnityAction restartGame;
    private Keybind keybindsMenu;
    private KeyCode pauseButton;
    private float initialTimeScale;

    // Use this for initialization
    void Start () {
        mainMenu = gameObject.GetComponent<MainMenu>();

        pauseTitle = GameObject.Find("PauseTitle");
        pausePanel = GameObject.Find("PausePanel");
        playerReady = GameObject.Find("PlayerReady");
        pauseMainMenuButton = GameObject.Find("PauseMainMenuButton");
        pauseResumeButton = GameObject.Find("PauseResumeButton");

        paused = false;
        pauseTitle.SetActive(false);
        pausePanel.SetActive(false);
        pauseMainMenuButton.SetActive(false);
        pauseResumeButton.SetActive(false);

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
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(pauseButton))
        {
            if (paused == false)
            {
                DisplayPauseMenu();
            }
            else
            {
                HidePauseMenu();
            }
        }
	}

    public void UpdatePauseButton()
    {
        pauseButton = keybindsMenu.GetPauseKey();
    }

    public void SetRestartGameAction(UnityAction restartGame)
    {
        this.restartGame = restartGame;
    }

    void DisplayPauseMenu()
    {
        initialTimeScale = Time.timeScale;
        Time.timeScale = 0;

        paused = true;
        pauseTitle.SetActive(true);
        pausePanel.SetActive(true);
        pauseMainMenuButton.SetActive(true);
        pauseResumeButton.SetActive(true);

        playerReadyState = playerReady.activeSelf;
        playerReady.SetActive(false);

        pauseMainMenuButton.GetComponent<Button>().onClick.AddListener(DisplayMainMenu);
        pauseResumeButton.GetComponent<Button>().onClick.AddListener(HidePauseMenu);
    }

    void HidePauseMenu()
    {
        Time.timeScale = initialTimeScale;

        paused = false;
        pauseTitle.SetActive(false);
        pausePanel.SetActive(false);
        pauseMainMenuButton.SetActive(false);
        pauseResumeButton.SetActive(false);

        playerReady.SetActive(playerReadyState);
    }

    void DisplayMainMenu()
    {
        HidePauseMenu();
        restartGame();
        mainMenu.DisplayMainMenu();
    }
}
