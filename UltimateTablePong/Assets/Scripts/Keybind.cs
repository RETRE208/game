using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keybind : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text p1Left, p1Right, pause, p2Left, p2Right, startButton;
    public float p1Sensitivity, p2Sensitivity;
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
        keys.Add("StartButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("StartButton", "Space")));

        p1Left.text = keys["P1 Left"].ToString();
        p1Right.text = keys["P1 Right"].ToString();
        pause.text = keys["Pause"].ToString();
        p2Left.text = keys["P2 Left"].ToString();
        p2Right.text = keys["P2 Right"].ToString();
        startButton.text = keys["StartButton"].ToString();

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

        p1Sensitivity = 2.0f;
        p2Sensitivity = 2.0f;
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
        if (Input.GetKeyDown(keys["StartButton"]))
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
                if (KeyAvailable(e.keyCode))
                {
                    keys[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                }
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
            if (isControlerInput())
            {
                if (KeyAvailable(e.keyCode))
                {
                    keys[currentKey.name] = GetControlerInput();
                    Debug.Log(keys[currentKey.name].ToString());
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = keys[currentKey.name].ToString();
                }
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
        DisplayMainMenu();
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

    public KeyCode GetStartButton()
    {
        return keys["StartButton"];
    }

    private bool KeyAvailable(KeyCode key)
    {
        return !(keys.ContainsValue(key));
    }

    private KeyCode GetControlerInput()
    {
        // joystick buttons
        if (Input.GetKey(KeyCode.JoystickButton0)) return KeyCode.JoystickButton0;
        if (Input.GetKey(KeyCode.JoystickButton1)) return KeyCode.JoystickButton1;
        if (Input.GetKey(KeyCode.JoystickButton2)) return KeyCode.JoystickButton2;
        if (Input.GetKey(KeyCode.JoystickButton3)) return KeyCode.JoystickButton3;
        if (Input.GetKey(KeyCode.JoystickButton4)) return KeyCode.JoystickButton4;
        if (Input.GetKey(KeyCode.JoystickButton5)) return KeyCode.JoystickButton5;
        if (Input.GetKey(KeyCode.JoystickButton6)) return KeyCode.JoystickButton6;
        if (Input.GetKey(KeyCode.JoystickButton7)) return KeyCode.JoystickButton7;
        if (Input.GetKey(KeyCode.JoystickButton8)) return KeyCode.JoystickButton8;
        if (Input.GetKey(KeyCode.JoystickButton9)) return KeyCode.JoystickButton9;
        if (Input.GetKey(KeyCode.JoystickButton10)) return KeyCode.JoystickButton10;
        if (Input.GetKey(KeyCode.JoystickButton11)) return KeyCode.JoystickButton11;
        if (Input.GetKey(KeyCode.JoystickButton12)) return KeyCode.JoystickButton12;
        if (Input.GetKey(KeyCode.JoystickButton13)) return KeyCode.JoystickButton13;
        if (Input.GetKey(KeyCode.JoystickButton14)) return KeyCode.JoystickButton14;
        if (Input.GetKey(KeyCode.JoystickButton15)) return KeyCode.JoystickButton15;
        if (Input.GetKey(KeyCode.JoystickButton16)) return KeyCode.JoystickButton16;
        if (Input.GetKey(KeyCode.JoystickButton17)) return KeyCode.JoystickButton17;
        if (Input.GetKey(KeyCode.JoystickButton18)) return KeyCode.JoystickButton18;
        if (Input.GetKey(KeyCode.JoystickButton19)) return KeyCode.JoystickButton19;
        return KeyCode.None;
    }

    private bool isControlerInput()
    {
        if (Input.GetKey(KeyCode.JoystickButton0) ||
           Input.GetKey(KeyCode.JoystickButton1) ||
           Input.GetKey(KeyCode.JoystickButton2) ||
           Input.GetKey(KeyCode.JoystickButton3) ||
           Input.GetKey(KeyCode.JoystickButton4) ||
           Input.GetKey(KeyCode.JoystickButton5) ||
           Input.GetKey(KeyCode.JoystickButton6) ||
           Input.GetKey(KeyCode.JoystickButton7) ||
           Input.GetKey(KeyCode.JoystickButton8) ||
           Input.GetKey(KeyCode.JoystickButton9) ||
           Input.GetKey(KeyCode.JoystickButton10) ||
           Input.GetKey(KeyCode.JoystickButton11) ||
           Input.GetKey(KeyCode.JoystickButton12) ||
           Input.GetKey(KeyCode.JoystickButton13) ||
           Input.GetKey(KeyCode.JoystickButton14) ||
           Input.GetKey(KeyCode.JoystickButton15) ||
           Input.GetKey(KeyCode.JoystickButton16) ||
           Input.GetKey(KeyCode.JoystickButton17) ||
           Input.GetKey(KeyCode.JoystickButton18) ||
           Input.GetKey(KeyCode.JoystickButton19))
        {
            return true;
        }
        return false;
    }

    public void setSlider1Value(Slider slider)
    {
        p1Sensitivity = slider.value;
    }

    public void setSlider2Value(Slider slider)
    {
        p2Sensitivity = slider.value;
    }
}
