using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatars : MonoBehaviour {

    private static readonly string FLOWER = "Flower";
    private static readonly string SWORD = "Sword";
    private static readonly string NORMAL = "Normal";

    List<string> models = new List<string> { NORMAL, FLOWER, SWORD };

    public GameObject normalStick;
    public GameObject flowerStick;
    public GameObject swordStick;

    public Dropdown player1Dropdown;
    public Dropdown player2Dropdown;

    private GameObject player1Avatar;
    private GameObject player2Avatar;

    private MainMenu mainMenu;
    private GameObject avatarMenu;

    private string player1AvatarModel;
    private string player2AvatarModel;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    private void Start()
    {
        avatarMenu = GameObject.FindGameObjectWithTag("AvatarMenu");
        avatarMenu.SetActive(false);

        player1Avatar = normalStick;
        player2Avatar = normalStick;

        player1AvatarModel = NORMAL;
        player2AvatarModel = NORMAL;

        GameObject MainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        if (MainMenu != null)
        {
            mainMenu = MainMenu.GetComponent<MainMenu>();
            if (mainMenu == null)
            {
                Debug.Log("Cannot find 'mainMenu' script");
            }
        }
        else
        {
            Debug.Log("Cannot find 'MainMenu' object");
        }

        scoreText = GameObject.Find("ScoreText");
        playerReady = GameObject.Find("PlayerReady");
        gameInfo = GameObject.Find("GameInfo");
        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);

        PopulateLists();
    }

    public void setPlayer1Avatar(int index)
    {
        string choosedModel = models[index];

        if (choosedModel.Equals(FLOWER))
        {
            player1Avatar = flowerStick;
            
        }
        else if (choosedModel.Equals(SWORD))
        {
            player1Avatar = swordStick;
        }
        else
        {
            player1Avatar = normalStick;
        }
        player1AvatarModel = choosedModel;
    }

    public void setPlayer2Avatar(int index)
    {
        string choosedModel = models[index];

        if (choosedModel.Equals(FLOWER))
        {
            player2Avatar = flowerStick;
        }
        else if (choosedModel.Equals(SWORD))
        {
            player2Avatar = swordStick;
        }
        else
        {
            player2Avatar = normalStick;
        }
        player2AvatarModel = choosedModel;
    }

    public string getPlayer1Avatar(out GameObject prefab)
    {
        prefab = player1Avatar;
        return player1AvatarModel;
    }

    public string getPlayer2Avatar(out GameObject prefab)
    {
        prefab = player2Avatar;
        return player2AvatarModel;
    }

    private void PopulateLists()
    {
        
        player1Dropdown.AddOptions(models);
        player2Dropdown.AddOptions(models);
    }

    public void DisplayMenu()
    {
        Time.timeScale = 0;
        avatarMenu.SetActive(true);

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);
    }

    public void HideAvatarMenu()
    {
        Time.timeScale = 1;
        avatarMenu.SetActive(false);

        scoreText.transform.localScale = new Vector3(1, 1, 1);
        playerReady.transform.localScale = new Vector3(2, 2, 2);
        gameInfo.transform.localScale = new Vector3(1, 1, 1);
    }

    public void DisplayMainMenu()
    {
        HideAvatarMenu();
        mainMenu.DisplayMainMenu();
    }
}
