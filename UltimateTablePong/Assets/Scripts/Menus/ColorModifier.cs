using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorModifier : MonoBehaviour {

    private CUIColorPicker cUIColorPicker;
    private GameObject colorPicker;
    private GameObject buttonSaveColor;
    private string currentColor;

    // Use this for initialization
    void Start () {
        Time.timeScale = 0;
        colorPicker = GameObject.FindGameObjectWithTag("ColorPicker");
        cUIColorPicker = colorPicker.GetComponent<CUIColorPicker>();

        colorPicker.SetActive(false);
    }

    public void EnableColorPicker(string name)
    {
        Time.timeScale = 1;
        currentColor = name;
        colorPicker.SetActive(true);
    }

    public void DisableColorPicker()
    {
        Time.timeScale = 1;
        GameObject button = GameObject.FindGameObjectWithTag(currentColor);

        cUIColorPicker = colorPicker.GetComponent<CUIColorPicker>();
        button.GetComponent<Image>().color = cUIColorPicker.Color;

        colorPicker.SetActive(false);
    }
}
