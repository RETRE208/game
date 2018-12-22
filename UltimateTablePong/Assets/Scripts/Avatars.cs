using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatars : MonoBehaviour {

    private static readonly string FLOWER = "Flower";
    private static readonly string SWORD = "Sword";
    private static readonly string NORMAL = "Normal";

    List<string> models = new List<string> { NORMAL, FLOWER, SWORD };

    public GameObject normalStickAvatar;
    public GameObject flowerStickAvatar;
    public GameObject swordStickAvatar;

    //The gameobject is different than the object shown because of a viewing bug.
    public GameObject flowerStick;

    public Dropdown player1Dropdown;
    public Dropdown player2Dropdown;

    private GameObject player1Avatar;
    private GameObject player2Avatar;

    private MainMenu mainMenu;
    private GameObject avatarMenu;

    private string player1AvatarModel;
    private string player2AvatarModel;

    private float player1AvatarSize;
    private float player2AvatarSize;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    private GameObject player1Cam;
    private GameObject player2Cam;

    private Color[] player1Colors = new Color[] {Color.clear, Color.clear, Color.clear, Color.clear};
    private Color[] player2Colors = new Color[] {Color.clear, Color.clear, Color.clear, Color.clear};

    private void Start()
    {
        player1Cam = GameObject.FindGameObjectWithTag("AvatarCam1");
        player2Cam = GameObject.FindGameObjectWithTag("AvatarCam2");

        avatarMenu = GameObject.FindGameObjectWithTag("AvatarMenu");
        avatarMenu.SetActive(false);

        player1Avatar = normalStickAvatar;
        player2Avatar = normalStickAvatar;

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

        player1AvatarSize = 300.0f;
        player2AvatarSize = 300.0f;

        PopulateLists();
    }

    public void setPlayer1Avatar(int index)
    {
        string choosedModel = models[index];
        float camPosX = player1Cam.transform.position.x;
        float camPosY = player1Cam.transform.position.y;
        float camPosZ = player1Cam.transform.position.z;

        if (choosedModel.Equals(FLOWER))
        {
            player1Avatar = flowerStick;
            camPosZ = 3500.0f;
            player1Cam.transform.position = new Vector3(camPosX, camPosY, camPosZ);
        }
        else if (choosedModel.Equals(SWORD))
        {
            player1Avatar = swordStickAvatar;
            Debug.Log(player1Avatar);
            camPosZ = 3000.0f;
            player1Cam.transform.position = new Vector3(camPosX, camPosY, camPosZ);
        }
        else
        {
            player1Avatar = normalStickAvatar;
            camPosZ = 4000.0f;
            player1Cam.transform.position = new Vector3(camPosX, camPosY, camPosZ);
        }
        player1AvatarModel = choosedModel;
    }

    public void setPlayer2Avatar(int index)
    {
        string choosedModel = models[index];
        float camPosX = player2Cam.transform.position.x;
        float camPosY = player2Cam.transform.position.y;
        float camPosZ = player2Cam.transform.position.z;

        if (choosedModel.Equals(FLOWER))
        {
            player2Avatar = flowerStick;
            camPosZ = 3500.0f;
            player2Cam.transform.position = new Vector3(camPosX, camPosY, camPosZ);
        }
        else if (choosedModel.Equals(SWORD))
        {
            player2Avatar = swordStickAvatar;
            camPosZ = 3000.0f;
            player2Cam.transform.position = new Vector3(camPosX, camPosY, camPosZ);
        }
        else
        {
            player2Avatar = normalStickAvatar;
            camPosZ = 4000.0f;
            player2Cam.transform.position = new Vector3(camPosX, camPosY, camPosZ);
        }
        player2AvatarModel = choosedModel;
    }

    public string getPlayer1Avatar(out GameObject prefab, out Color[] colors, out float size)
    {
        prefab = player1Avatar;
        colors = player1Colors;
        size = player1AvatarSize;
        return player1AvatarModel;
    }

    public string getPlayer2Avatar(out GameObject prefab, out Color[] colors, out float size)
    {
        prefab = player2Avatar;
        colors = player2Colors;
        size = player2AvatarSize;
        return player2AvatarModel;
    }

    private void PopulateLists()
    {
        
        player1Dropdown.AddOptions(models);
        player2Dropdown.AddOptions(models);
    }

    public void DisplayMenu()
    {
        avatarMenu.SetActive(true);

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);
    }

    public void HideAvatarMenu()
    {
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

    public void setColor(int playerNumber, int colorNumber, Color color)
    {
        if (playerNumber == 1)
        {
            player1Colors[colorNumber - 1] = color;
            
            if (colorNumber == 1)
            {
                GameObject.FindGameObjectWithTag("StickAvatarExemple").GetComponent<Renderer>().material.color = color;
            }

            var flowerChildren = GameObject.FindGameObjectWithTag("FlowerAvatarExemple").GetComponentsInChildren<Transform>();
            foreach (var child in flowerChildren)
            {
                if (child.name.Equals("Stem"))
                {
                    if (colorNumber == 1)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Middle"))
                {
                    if (colorNumber == 2)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Petals"))
                {
                    if (colorNumber == 3)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
            }

            var swordChildren = GameObject.FindGameObjectWithTag("SwordAvatarExemple").GetComponentsInChildren<Transform>();
            foreach (var child in swordChildren)
            {
                if (child.name.Equals("Blade"))
                {
                    if (colorNumber == 1)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Crossguard"))
                {
                    if (colorNumber == 2)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Grip"))
                {
                    if (colorNumber == 3)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Pomel"))
                {
                    if (colorNumber == 4)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
            }
        }
        else
        {
            player2Colors[colorNumber - 1] = color;

            if (colorNumber == 1)
            {
                GameObject.FindGameObjectWithTag("StickAvatarExemple2").GetComponent<Renderer>().material.color = color;
            }

            var flowerChildren = GameObject.FindGameObjectWithTag("FlowerAvatarExemple2").GetComponentsInChildren<Transform>();
            foreach (var child in flowerChildren)
            {
                if (child.name.Equals("Stem"))
                {
                    if (colorNumber == 1)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Middle"))
                {
                    if (colorNumber == 2)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Petals"))
                {
                    if (colorNumber == 3)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
            }

            var swordChildren = GameObject.FindGameObjectWithTag("SwordAvatarExemple2").GetComponentsInChildren<Transform>();
            foreach (var child in swordChildren)
            {
                if (child.name.Equals("Blade"))
                {
                    if (colorNumber == 1)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Crossguard"))
                {
                    if (colorNumber == 2)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Grip"))
                {
                    if (colorNumber == 3)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
                else if (child.name.Equals("Pomel"))
                {
                    if (colorNumber == 4)
                    {
                        child.GetComponent<Renderer>().material.color = color;
                    }
                }
            }
        }
    }

    public void setPlayer1Size(float size)
    {
        Vector3 stickSize;

        player1AvatarSize = size;

        stickSize = GameObject.FindGameObjectWithTag("StickAvatarExemple").GetComponent<Transform>().localScale;
        stickSize.z = size;
        GameObject.FindGameObjectWithTag("StickAvatarExemple").GetComponent<Transform>().localScale = stickSize;

        stickSize = GameObject.FindGameObjectWithTag("FlowerAvatarExemple").GetComponent<Transform>().localScale;
        stickSize.z = size;
        GameObject.FindGameObjectWithTag("FlowerAvatarExemple").GetComponent<Transform>().localScale = stickSize;

        stickSize = GameObject.FindGameObjectWithTag("SwordAvatarExemple").GetComponent<Transform>().localScale;
        stickSize.z = size;
        GameObject.FindGameObjectWithTag("SwordAvatarExemple").GetComponent<Transform>().localScale = stickSize;
    }

    public void setPlayer2Size(float size)
    {
        Vector3 stickSize;

        player2AvatarSize = size;

        stickSize = GameObject.FindGameObjectWithTag("StickAvatarExemple2").GetComponent<Transform>().localScale;
        stickSize.z = size;
        GameObject.FindGameObjectWithTag("StickAvatarExemple2").GetComponent<Transform>().localScale = stickSize;

        stickSize = GameObject.FindGameObjectWithTag("FlowerAvatarExemple2").GetComponent<Transform>().localScale;
        stickSize.z = size;
        GameObject.FindGameObjectWithTag("FlowerAvatarExemple2").GetComponent<Transform>().localScale = stickSize;

        stickSize = GameObject.FindGameObjectWithTag("SwordAvatarExemple2").GetComponent<Transform>().localScale;
        stickSize.z = size;
        GameObject.FindGameObjectWithTag("SwordAvatarExemple2").GetComponent<Transform>().localScale = stickSize;
    }
}
