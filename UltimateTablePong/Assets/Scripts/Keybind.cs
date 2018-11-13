using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keybind : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text p1Left, p1Right, pause, p2Left, p2Right;
    private GameObject currentKey;
    private Color32 normal = new Color32(255, 255, 255, 255);
    private Color32 selected = new Color32(176, 176, 176, 255);

    private GameObject keybindsMenu;
    private MainMenu mainMenu;

    private GameObject scoreText;
    private GameObject playerReady;
    private GameObject gameInfo;

    // Use this for initialization
    void Start () {
        keys.Add("P1 Left", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("P1 Left", "A")));
        keys.Add("P1 Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("P1 Right", "D")));
        keys.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "Escape")));
        keys.Add("P2 Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("P2 Left", "LeftArrow")));
        keys.Add("P2 Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("P2 Right", "RightArrow")));
        
        p1Left.text = keys["P1 Left"].ToString();
        p1Right.text = keys["P1 Right"].ToString();
        pause.text = keys["Pause"].ToString();
        p2Left.text = keys["P2 Left"].ToString();
        p2Right.text = keys["P2 Right"].ToString();

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

        Time.timeScale = 0;

        keybindsMenu = GameObject.Find("SettingKeybinds");
        keybindsMenu.SetActive(false);

        scoreText = GameObject.Find("ScoreText");
        playerReady = GameObject.Find("PlayerReady");
        gameInfo = GameObject.Find("GameInfo");
        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);

    }

    public void DisplayKeybindMenu()
    {
        Time.timeScale = 0;
        keybindsMenu.SetActive(true);

        scoreText.transform.localScale = new Vector3(0, 0, 0);
        playerReady.transform.localScale = new Vector3(0, 0, 0);
        gameInfo.transform.localScale = new Vector3(0, 0, 0);

    }

    public void HideKeybindMenu()
    {
        Time.timeScale = 1;
        keybindsMenu.SetActive(false);

        scoreText.transform.localScale = new Vector3(1, 1, 1);
        playerReady.transform.localScale = new Vector3(2, 2, 2);
        gameInfo.transform.localScale = new Vector3(1, 1, 1);
    }

    public void DisplayMainMenu()
    {
        HideKeybindMenu();
        mainMenu.DisplayMainMenu();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(keys["P1 Left"]))
        {

        }
        if (Input.GetKeyDown(keys["P1 Right"]))
        {

        }
        if (Input.GetKeyDown(keys["Pause"]))
        {

        }
        if (Input.GetKeyDown(keys["P2 Left"]))
        {

        }
        if (Input.GetKeyDown(keys["P2 Right"]))
        {

        }
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    public KeyCode GetP1LeftKey()
    {
        return keys["P1 Left"];
    }

    public KeyCode GetP1RightKey()
    {
        return keys["P1 Right"];
    }

    public KeyCode GetP2LeftKey()
    {
        return keys["P2 Left"];
    }

    public KeyCode GetP2RightKey()
    {
        return keys["P2 Right"];
    }

    public KeyCode GetPauseKey()
    {
        return keys["Pause"];
    }
}
