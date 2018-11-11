using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keybind : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text p1Left, p1Right, pause, p2Left, p2Right;
    private GameObject currentKey;
    private Color32 normal = new Color(255, 255, 255);
    private Color32 selected = new Color(176, 176, 176);

    // Use this for initialization
    void Start () {
        keys.Add("P1 Left", KeyCode.A);
        keys.Add("P1 Right", KeyCode.D);
        keys.Add("Pause", KeyCode.Escape);
        keys.Add("P2 Left", KeyCode.LeftArrow);
        keys.Add("P2 Right", KeyCode.RightArrow);

        p1Left.text = keys["P1 Left"].ToString();
        p1Right.text = keys["P1 Right"].ToString();
        pause.text = keys["Pause"].ToString();
        p2Left.text = keys["P2 Left"].ToString();
        p2Right.text = keys["P2 Right"].ToString();
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

}
