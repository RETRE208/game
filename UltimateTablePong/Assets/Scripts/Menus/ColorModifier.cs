using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorModifier : MonoBehaviour {

    private CUIColorPicker cUIColorPicker;
    private GameObject colorPicker;
    private GameObject buttonSaveColor;
    private string currentColorBeingModified;

    // Use this for initialization
    void Start () {
        colorPicker = GameObject.FindGameObjectWithTag("ColorPicker");
        cUIColorPicker = colorPicker.GetComponent<CUIColorPicker>();

        colorPicker.SetActive(false);
    }

    public void EnableColorPicker(string name)
    {
        currentColorBeingModified = name;
        colorPicker.SetActive(true);
    }

    public void DisableColorPicker()
    {
        GameObject button = GameObject.FindGameObjectWithTag(currentColorBeingModified);

        cUIColorPicker = colorPicker.GetComponent<CUIColorPicker>();
        button.GetComponent<Image>().color = cUIColorPicker.Color;

        setAvatarColor(cUIColorPicker.Color);

        colorPicker.SetActive(false);
    }

    private void setAvatarColor(Color color)
    {
        GameObject avatars = GameObject.FindGameObjectWithTag("Avatars");
        Avatars avatarsScript = avatars.GetComponent<Avatars>();
        switch (currentColorBeingModified) {

            case "ButtonP1C1":
                avatarsScript.setColor(1, 1, color);
                break;

            case "ButtonP1C2":
                avatarsScript.setColor(1, 2, color);
                break;

            case "ButtonP1C3":
                avatarsScript.setColor(1, 3, color);
                break;

            case "ButtonP1C4":
                avatarsScript.setColor(1, 4, color);
                break;

            case "ButtonP2C1":
                avatarsScript.setColor(2, 1, color);
                break;

            case "ButtonP2C2":
                avatarsScript.setColor(2, 2, color);
                break;

            case "ButtonP2C3":
                avatarsScript.setColor(2, 3, color);
                break;

            case "ButtonP2C4":
                avatarsScript.setColor(2, 4, color);
                break;

            default:
                break; 
        }
    }
}
