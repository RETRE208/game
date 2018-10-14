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
    public GameObject pucks;
    public float startWait;
    public float spawnWait;
    public Vector3 spawnValues;
    public CameraController cam;

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
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        UpdateScore();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //cam.MoveCameraToOtherPlayerField();
            playerReadyText.gameObject.SetActive(false);
            StartCoroutine(SpawnPucks());
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

}
