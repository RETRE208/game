using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    private GameObject loadingPanel;
    private GameObject loadingText;
    private GameObject loadingProgress;
    private GameObject arcadeAmbiance;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    private float progressionValue;
    private bool loadingStatus;

    private SoundManager soundManager;

	// Use this for initialization
	void Start () {
        arcadeAmbiance = GameObject.Find("ArcadeAmbiance");
        soundManager = FindObjectOfType<SoundManager>();
        loadingStatus = false;
        progressionValue = 0;

        loadingPanel = GameObject.Find("LoadingPanel");
        loadingText = GameObject.Find("LoadingText");
        loadingProgress = GameObject.Find("LoadingProgress");

        scoreText = GameObject.Find("ScoreText");
        playerReady = GameObject.Find("PlayerReady");
        gameInfo = GameObject.Find("GameInfo");

        loadingPanel.SetActive(false);
        loadingText.SetActive(false);
        loadingProgress.SetActive(false);
        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (loadingStatus)
        {
            progressionValue = progressionValue + 1;
            loadingProgress.GetComponent<Text>().text = progressionValue + "%";
            if (getIfLoadingDone())
            {
                hideLoadingScreen();
            }
        }
        
	}

    private void activateLoading()
    {
        loadingStatus = true;
    }

    public bool getIfLoadingDone()
    {
        if (progressionValue >= 100)
        {
            progressionValue = 0;
            loadingStatus = false;
            return true;
        }
        return false;
    }

    public void displayLoadingScreen()
    {
        loadingPanel.SetActive(true);
        loadingText.SetActive(true);
        loadingProgress.SetActive(true);
        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);
        activateLoading();
    }

    public void hideLoadingScreen()
    {
        loadingPanel.SetActive(false);
        loadingText.SetActive(false);
        loadingProgress.SetActive(false);
        scoreText.transform.localScale = new Vector3(1, 1, 1);
        playerReady.transform.localScale = new Vector3(1, 1, 1);
        gameInfo.transform.localScale = new Vector3(1, 1, 1);

        soundManager.playMusic("inGame");
        arcadeAmbiance.GetComponent<AudioSource>().volume = soundManager.ambiantVolume;
        arcadeAmbiance.GetComponent<AudioSource>().Play();
    }
}
