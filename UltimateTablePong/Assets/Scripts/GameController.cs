using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public Text playerReadyText;
    public int scorePlayer1;
    public int scorePlayer2;
    public int puckCount;
    public int puckReady;
    public GameObject pucks;
    public float startWait;
    public float spawnWait;
    public Vector3 spawnValues;
    public CameraController cam;
    public bool player1turn;
    public List<Puck> puckList;

    // Use this for initialization
    void Start () {
        GameObject camObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (camObject != null)
        {
            cam = camObject.GetComponent<CameraController>();
            if (cam == null)
            {
                Debug.Log("Cannot find 'Camera' script");
            }
        } else
        {
            Debug.Log("Cannot find 'MainCamera' object");
        }
        player1turn = true;
        puckReady = 0;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        UpdateScore();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerReadyText.gameObject.SetActive(false);
            StartCoroutine(SpawnPucks());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnPucks()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < puckCount; i++)
            {
                GameObject puck = pucks;
                Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(puck, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(spawnWait);
            break;
        }
        GetPucks();
    }

    void UpdateScore () {
        scoreText.text = "Score Player 1 : " + scorePlayer1 + " Player 2 : " + scorePlayer2;
    }

    public void AddScorePlayer1(int newScoreValue)
    {
        scorePlayer1 += newScoreValue;
        UpdateScore();
    }

    public void AddScorePlayer2(int newScoreValue)
    {
        scorePlayer2 += newScoreValue;
        UpdateScore();
    }

    void ChangeSides()
    {
        if (player1turn)
        {
            player1turn = false;
        }
        else
        {
            player1turn = true;
        }

        cam.MoveCameraToOtherPlayerField();

        if (player1turn)
        {
            playerReadyText.text = "Player one's turn, \n Press space to start!";
        }
        else
        {
            playerReadyText.text = "Player two's turn, \n Press space to start!";
        }
        playerReadyText.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        puckList = new List<Puck>();
        GetPucks();
        Debug.Log(puckList.Count);

        playerReadyText.gameObject.SetActive(false);
        for (int i = 0; i < puckList.Count; i++)
        {
            puckList[i].player1Turn = player1turn;
            puckList[i].puckIsReady = false;
            Debug.Log("Puck Done " + i);
        }
    }

    void GetPucks()
    {
        GameObject[] liste = GameObject.FindGameObjectsWithTag("Puck");
        for(int i = 0; i < liste.Length; i++){

            if (liste != null)
            {
                puckList.Add(liste[0].GetComponent<Puck>());
                if (puckList.Count == 0)
                {
                    Debug.Log("Liste vide");
                }
            }
        }
    }

    public void PuckIsReady()
    {
        puckReady += 1;
        if (puckReady == puckCount)
        {
            puckReady = 0;
            ChangeSides();
        }
    }

    public void PuckIsDestroy(Puck puck)
    {
        Debug.Log(puckList.Count);
        puckList.Remove(puck);
        Debug.Log(puckList.Count);
        if(puckList.Count == 0)
        {
            player1turn = true;
            if (cam.currentPlayer != 1)
            {
                cam.MoveCameraToOtherPlayerField();
            }
            playerReadyText.gameObject.SetActive(true);
            playerReadyText.text = "Player one's turn, \n Press 'F' to start!";
        }
    }

}
